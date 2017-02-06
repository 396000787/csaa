using System;
using System.Text;
using System.Collections.Generic;
using TinyStack.Modules.iUtility.InterFaceUtility;
using System.IO;
using System.Security.Cryptography;

namespace TinyStack.Modules.iUtility.iTools
{
    public class EncryptDecryption : IEncryptDecryption
    {
        private const ulong FC_TAG = 0xFC010203040506CF;
        private const int BUFFER_SIZE = 128 * 1024;

        #region 加密字符串
        /// <summary>
        /// 字符串加密算法
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="negative">是否不可逆，true为可逆，false为不可逆</param>
        /// <param name="key">密钥，不可逆可为空，可逆为8位字符串</param>
        /// <returns></returns>
        public string EncryptStr(string str, bool negative, string key)
        {

            bool getnegative = negative;
            //判断是否可逆加密
            if (getnegative == false)
            {
                //不可逆加密方法，调用MD5加密算法
                string result = StringToMD5(str);
                //返回加密后字符串
                return result;
            }
            else
            {
                //可逆算法调用DES算法
                string result = DesEncrypt(str, key);
                //返回加密后字符串
                return result;
            }
        }
        #endregion

        #region DES解密字符串
        /// <summary>
        /// DES解密算法
        /// Key为8位字符串
        /// </summary>
        /// <param name="str">需要解密的字符串</param>
        /// <param name="key">密钥，密钥为8位字符串</param>
        /// <returns></returns>
        public string DESDecryptStr(string str, string key)
        {
            try
            {
                //实例化加密类
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //获取密码字节数组
                byte[] inputByteArray = new byte[str.Length / 2];
                //循环转换字节数组
                for (int x = 0; x < str.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(str.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                //密钥转换成ASCII编码
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                //初始化向量
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);
                //创建内存流
                MemoryStream ms = new MemoryStream();
                //定义加密转换流
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                //清理缓存
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                //返回解密后字符串
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 文件DES加密
        /// <summary>
        /// 文件DES加密算法
        /// </summary>
        /// <param name="inFile">需要解密的文件</param>
        /// <param name="outFile">加密后文件存放位置</param>
        /// <param name="key">密钥，密钥为8位字符串</param>
        /// <returns></returns>
        public bool EncryptFile(string inFile, string outFile, string key)
        {
            //初始化结果为false
            bool result = false;
            try
            {
                using (FileStream fin = File.OpenRead(inFile),
                fout = File.OpenWrite(outFile))
                {
                    // 输入文件长度
                    long lSize = fin.Length; 
                    int size = (int)lSize;
                    // 缓存
                    byte[] bytes = new byte[BUFFER_SIZE];
                    // 输入文件读取数量
                    int read = -1;
                    int value = 0;
                    // 获取IV和salt
                    byte[] IV = GenerateRandomBytes(16);
                    byte[] salt = GenerateRandomBytes(16);
                    // 创建加密对象
                    SymmetricAlgorithm sma = CreateRijndael(key, salt);
                    sma.IV = IV;
                    // 在输出文件开始部分写入IV和salt
                    fout.Write(IV, 0, IV.Length);
                    fout.Write(salt, 0, salt.Length);
                    // 创建散列加密
                    HashAlgorithm hasher = SHA256.Create();
                    using (CryptoStream cout = new CryptoStream(fout, sma.CreateEncryptor(), CryptoStreamMode.Write),
                        chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                    {
                        BinaryWriter bw = new BinaryWriter(cout);
                        bw.Write(lSize);
                        bw.Write(FC_TAG);
                        // 读写字节块到加密流缓冲区
                        while ((read = fin.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            cout.Write(bytes, 0, read);
                            chash.Write(bytes, 0, read);
                            value += read;
                        }
                        // 关闭加密流
                        chash.Flush();
                        chash.Close();
                        // 读取散列
                        byte[] hash = hasher.Hash;
                        // 输入文件写入散列
                        cout.Write(hash, 0, hash.Length);
                        // 关闭文件流
                        cout.Flush();
                        cout.Close();
                        //加密结束后重置返回结果为true
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //返回执行结果
            return result;
        }
        #endregion

        #region 文件DES解密
        /// <summary>
        /// 文件DES解密算法
        /// </summary>
        /// <param name="inFile">需要解密的文件</param>
        /// <param name="outFile">解密后文件存放位置</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public bool DecryptFile(string inFile, string outFile, string key)
        {
            //初始化返回结果为false
            bool result = false;
            try
            {
                // 创建打开文件流
                using (FileStream fin = File.OpenRead(inFile), fout = File.OpenWrite(outFile))
                {
                    // 输入文件长度
                    int size = (int)fin.Length;
                    // 缓存
                    byte[] bytes = new byte[BUFFER_SIZE];
                    // 输入文件读取数量
                    int read = -1;
                    int value = 0;
                    int outValue = 0;
                    byte[] IV = new byte[16];
                    fin.Read(IV, 0, 16);
                    byte[] salt = new byte[16];
                    fin.Read(salt, 0, 16);
                    SymmetricAlgorithm sma = CreateRijndael(key, salt);
                    sma.IV = IV;
                    value = 32;
                    long lSize = -1;
                    // 创建散列对象, 校验文件
                    HashAlgorithm hasher = SHA256.Create();
                    using (CryptoStream cin = new CryptoStream(fin, sma.CreateDecryptor(), CryptoStreamMode.Read), chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                    {
                        // 读取文件长度
                        BinaryReader br = new BinaryReader(cin);
                        lSize = br.ReadInt64();
                        ulong tag = br.ReadUInt64();
                        if (FC_TAG != tag)
                            throw new CryptoHelpException("文件被破坏");
                        long numReads = lSize / BUFFER_SIZE;
                        long slack = (long)lSize % BUFFER_SIZE;
                        for (int i = 0; i < numReads; ++i)
                        {
                            read = cin.Read(bytes, 0, bytes.Length);
                            fout.Write(bytes, 0, read);
                            chash.Write(bytes, 0, read);
                            value += read;
                            outValue += read;
                        }
                        if (slack > 0)
                        {
                            read = cin.Read(bytes, 0, (int)slack);
                            fout.Write(bytes, 0, read);
                            chash.Write(bytes, 0, read);
                            value += read;
                            outValue += read;
                        }
                        chash.Flush();
                        chash.Close();
                        fout.Flush();
                        fout.Close();
                        byte[] curHash = hasher.Hash;
                        // 获取比较和旧的散列对象
                        byte[] oldHash = new byte[hasher.HashSize / 8];
                        read = cin.Read(oldHash, 0, oldHash.Length);
                        if ((oldHash.Length != read) || (!CheckByteArrays(oldHash, curHash)))
                        {
                            throw new CryptoHelpException("文件被破坏");
                        }
                    }
                    if (outValue != lSize)
                    {
                        throw new CryptoHelpException("文件大小不匹配");
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //返回执行结果
            return result;
        }
        #endregion

        #region 生成指定长度的随机Byte数组
        /// <summary>
        /// 生成指定长度的随机Byte数组
        /// </summary>
        /// <param name="count">Byte数组长度</param>
        /// <returns>随机Byte数组</returns>
        private static byte[] GenerateRandomBytes(int count)
        {
            RandomNumberGenerator rand = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[count];
            rand.GetBytes(bytes);
            return bytes;
        }
        #endregion

        #region 创建Rijndael SymmetricAlgorithm
        /// <summary>
        /// 创建Rijndael SymmetricAlgorithm
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="salt"></param>
        /// <returns>加密对象</returns>
        private static SymmetricAlgorithm CreateRijndael(string password, byte[] salt)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt, "SHA256", 1000);
            SymmetricAlgorithm sma = Rijndael.Create();
            sma.KeySize = 256;
            sma.Key = pdb.GetBytes(32);
            sma.Padding = PaddingMode.PKCS7;
            return sma;
        }
        #endregion

        #region 异常报错类
        /// <summary>
        /// 异常处理类
        /// </summary>
        public class CryptoHelpException : ApplicationException
        {
            //输出报错信息
            public CryptoHelpException(string msg) : base(msg) { }
        }
        #endregion

        #region Byte数据校验
        /// <summary>
        /// 检验两个Byte数组是否相同
        /// </summary>
        /// <param name="b1">Byte数组</param>
        /// <param name="b2">Byte数组</param>
        /// <returns>true－相等</returns>
        private static bool CheckByteArrays(byte[] b1, byte[] b2)
        {
            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; ++i)
                {
                    if (b1[i] != b2[i])
                        return false;
                }
                return true;
            }
            return false;
        }
        #endregion

        #region DES加密字符串
        /// <summary>
        /// DES加密
        /// sKey为8位或16位
        /// </summary>
        /// <param name="pToEncrypt">需要加密的字符串</param>
        /// <param name="sKey">密钥，密钥为8位字符串</param>
        /// <returns></returns>
        public string DesEncrypt(string pToEncrypt, string sKey)
        {
            if (sKey.Length == 8)
            {
                try
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
                    des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                    des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    StringBuilder ret = new StringBuilder();
                    foreach (byte b in ms.ToArray())
                    {
                        ret.AppendFormat("{0:X2}", b);
                    }
                    ret.ToString();
                    return ret.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return ("密钥长度应为8位字符");
            }
        }
        #endregion

        #region MD5加密字符串
        /// <summary>
        /// 对字符串进行MD5加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>返回加密后字符串</returns>
        public static string StringToMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(str), 0, str.Length); 
            char[] temp = new char[res.Length]; 
            System.Array.Copy(res, temp, res.Length); 
            return new String(temp);
        }

        #endregion

        #region MD5加密字符串
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(result).Replace("-", "").ToUpper();
        }
        #endregion
    }
}