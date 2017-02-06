using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib;
using TinyStack.Modules.iUtility.InterFaceUtility;
using Microsoft.Win32;
using Newtonsoft.Json;

using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Checksums;



namespace TinyStack.Modules.iUtility.iTools
{
    public class SizeOperate : ISizeOperate
    {
        #region 文件压缩方法
        /// <summary>
        /// 文件压缩方法
        /// </summary>
        /// <param name="filepath">源文件地址</param>
        /// <param name="rarpath">压缩后存放地址</param>
        /// <param name="rarname">压缩包名字</param>
        /// <param name="del">是否删除源文件，true为删除，false为不删除</param>
        /// <param name="way">压缩方式，true为rar方式压缩，false为zip方式压缩</param>
        /// <returns>压缩是否成功，true为成功，失败报错</returns>
        public bool Compress(string filepath, string rarpath, string rarname, bool del, bool way)
        {
            bool getway;
            bool getdel;
            getdel = del;
            getway = way;
            //判断文件夹是否存在
            if (!Directory.Exists(rarpath))
            {
                Directory.CreateDirectory(rarpath);
            }
            //判断zip压缩方式压缩文件
            if (getway == false)
            {
                //获取zip压缩结果
                bool result = ZipFileMain(filepath, rarpath, rarname);
                //判断压缩结果
                if (result == true)
                {
                    //判断是否需要删除源文件
                    if (getdel == false)
                    {
                        //不删除源文件返回true
                        return true;
                    }
                    else
                    {
                        //删除源文件并获取结果
                        bool delresult = DeleteFolder(filepath);
                        if (delresult == true)
                        {
                            //删除成功返回true
                            return true;
                        }
                        else
                        {
                            //删除不成功返回false
                            return false;
                        }
                    }
                }
                else
                {
                    //压缩失败返回false
                    return false;
                }
            }
            //使用RAR方式压缩文件
            else
            {
                //判断是否安装压缩包
                Exists();
                //调用RAR压缩方法获取结果
                bool result = CompressRAR(filepath, rarpath, rarname);
                if (result == true)
                {
                    //判断是否删除源文件
                    if (getdel == false)
                    {
                        //不删除返回true
                        return true;
                    }
                    else
                    {
                        //调用删除方法获取返回结果
                        bool delresult = DeleteFolder(filepath);
                        if (delresult == true)
                        {
                            //删除成功返回true
                            return true;
                        }
                        else
                        {
                            //删除失败返回false
                            return false;
                        }
                    }
                }
                else
                {
                    //压缩失败返回false
                    return false;
                }
            }
        }
        #endregion

        #region 分段压缩
        /// <summary>
        /// 分段压缩
        /// </summary>
        /// <param name="filepath">需压缩文件路径</param>
        /// <param name="rarpath">压缩后存放路径</param>
        /// <param name="rarname">压缩包名称</param>
        /// <param name="del">是否删除源文件true为删除，false为不删除</param>
        /// <param name="datano">分包大小，单位为K</param>
        /// <returns></returns>
        public bool SecCompressRAR(string filepath, string rarpath, string rarname, bool del, int datano)
        {
            //判断是否安装压缩包
            Exists();
            //定义参数
            string the_rar;
            RegistryKey the_Reg;
            object the_Obj;
            string the_Info;
            bool delvalue;
            int size = datano;
            delvalue = del;
            bool result = false;
            string _rarpath = Path.Combine(rarpath, rarname);

            if (delvalue == false)
            {
                //不删除源文件执行以下操作
                try
                {
                    //获取winrar安装路径
                    the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                    the_Obj = the_Reg.GetValue("");
                    the_rar = the_Obj.ToString();
                    the_Reg.Close();

                    if (Directory.Exists(filepath) == false)
                    {
                        //如果不存在存放路径则创建文件
                        Directory.CreateDirectory(filepath);
                    }
                    //执行命令语句拼接
                    the_Info = @"a -v" + size + "K -ibck -ep1 " + _rarpath + " " + filepath + " -y";

                    //命令初始化
                    ProcessStartInfo the_StartInfo = new ProcessStartInfo();
                    the_StartInfo.FileName = the_rar;
                    the_StartInfo.Arguments = the_Info;
                    the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    the_StartInfo.WorkingDirectory = rarpath;//获取压缩包路径

                    Process the_Process = new Process();
                    the_Process.StartInfo = the_StartInfo;
                    the_Process.Start();
                    the_Process.WaitForExit();
                    the_Process.Close();
                    //执行命令完成后返回结果
                    result = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                    //return false;
                }
                //返回压缩结果
                return result;
            }
            else
            {
                //删除源文件执行以下语句
                try
                {
                    //获取winrar安装路径
                    the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                    the_Obj = the_Reg.GetValue("");
                    the_rar = the_Obj.ToString();
                    the_Reg.Close();

                    if (Directory.Exists(filepath) == false)
                    {
                        //不存在文件存放路径则创建文件夹
                        Directory.CreateDirectory(filepath);
                    }
                    //命令语句拼接
                    the_Info = @"a -v" + size + "K -ibck -ep1 " + _rarpath + " " + filepath + " -y";

                    //命令初始化
                    ProcessStartInfo the_StartInfo = new ProcessStartInfo();
                    the_StartInfo.FileName = the_rar;
                    the_StartInfo.Arguments = the_Info;
                    the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    the_StartInfo.WorkingDirectory = rarpath;//获取压缩包路径

                    Process the_Process = new Process();
                    the_Process.StartInfo = the_StartInfo;
                    the_Process.Start();
                    the_Process.WaitForExit();
                    the_Process.Close();
                    //调用删除源文件方法
                    bool delresult = DeleteFolder(filepath);
                    if (delresult == true)
                    {
                        //删除成功返回true
                        result = true;
                    }
                    else
                    {
                        //删除失败返回false
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    //return false;
                }
                //返回压缩结果
                return result;
            }

        }
        #endregion

        #region 解压缩文件文件
        /// <summary>
        /// 解压Rar文件夹
        /// </summary>
        /// <param name="filepath">解压后保存的文件夹路径</param>
        /// <param name="rarpath">压缩文件保存路径</param>
        /// <param name="rarname">压缩文件名（完全路径）</param>
        /// <param name="del">是否删除源文件，true为删除，false为不删除</param>
        /// <returns>解压成功返回true，失败抛错</returns>
        public bool UnCompressRAR(string filepath, string rarpath, string rarname, bool del)
        {
            //校验是否安装winrar
            Exists();
            //定义参数
            string the_rar;
            RegistryKey the_Reg;
            object the_Obj;
            string the_Info;
            bool delvalue;
            delvalue = del;
            bool result = false;
            string _rarpath = Path.Combine(rarpath, rarname);

            if (delvalue == false)
            {
                //如果不删除源文件执行以下语句
                try
                {
                    //获取winrar安装路径
                    the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                    the_Obj = the_Reg.GetValue("");
                    the_rar = the_Obj.ToString();
                    the_Reg.Close();

                    if (File.Exists(_rarpath) == false)
                    {
                        //如果压缩包不存在，返回false
                        result = false;
                    }
                    if (Directory.Exists(filepath) == false)
                    {
                        //如果解压缩后存放文件路径则创建文件夹
                        Directory.CreateDirectory(filepath);
                    }
                    else
                    {
                        //命令拼接
                        the_Info = @"x -ibck " + _rarpath + " " + filepath + " -y";
                        //命令初始化
                        ProcessStartInfo the_StartInfo = new ProcessStartInfo();
                        the_StartInfo.FileName = the_rar;
                        the_StartInfo.Arguments = the_Info;
                        the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        the_StartInfo.WorkingDirectory = rarpath;//获取压缩包路径

                        Process the_Process = new Process();
                        the_Process.StartInfo = the_StartInfo;
                        the_Process.Start();
                        the_Process.WaitForExit();
                        the_Process.Close();
                        //命令执行完成后结果赋值为true
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //返回执行结果
                return result;
            }
            else
            {
                //如果执行删除源文件，执行以下语句
                try
                {
                    //获取winrar安装路径
                    the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                    the_Obj = the_Reg.GetValue("");
                    the_rar = the_Obj.ToString();
                    the_Reg.Close();
                    //判断压缩包是否存在
                    if (File.Exists(_rarpath) == false)
                    {
                        result = false;
                    }
                    //判断存放文件夹是否存在，不存在则创建
                    if (Directory.Exists(filepath) == false)
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    else
                    {
                        //命令拼接
                        the_Info = @"x -ibck " + _rarpath + " " + filepath + " -y";
                        //命令初始化
                        ProcessStartInfo the_StartInfo = new ProcessStartInfo();
                        the_StartInfo.FileName = the_rar;
                        the_StartInfo.Arguments = the_Info;
                        the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        the_StartInfo.WorkingDirectory = rarpath;//获取压缩包路径

                        Process the_Process = new Process();
                        the_Process.StartInfo = the_StartInfo;
                        the_Process.Start();
                        the_Process.WaitForExit();
                        the_Process.Close();
                        //调用删除源文件方法
                        File.Delete(_rarpath);
                        //执行完命令后重置结果为true
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //返回执行结果
                return result;
            }
        }
        #endregion

        #region 删除源文件
        /// <summary>
        /// 删除源文件
        /// </summary>
        /// <param name="filepath">删除的源文件地址</param>
        /// <return>删除成功返回true，失败报错，返回false</return>
        public static bool DeleteFolder(string filepath)
        {
            bool result = false;
            try
            {
                //获取文件夹子目录
                foreach (string d in Directory.GetFileSystemEntries(filepath))
                {
                    if (File.Exists(d))
                    {
                        FileInfo fi = new FileInfo(d);
                        //判断文件是否为只读文件，只读文件重置属性
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                        //直接删除其中的文件     
                        File.Delete(d);
                    }
                    else
                    {
                        //递归删除子文件夹    
                        DeleteFolder(d);
                    }
                }
                DirectoryInfo di = new DirectoryInfo(filepath);
                //判断文件是否为只读文件，只读文件重置属性
                if (di.Attributes.ToString().IndexOf("ReadOnly") != -1)
                {
                    di.Attributes = FileAttributes.Normal;
                }
                //删除已空文件夹     
                Directory.Delete(filepath);
                //重置返回结果为true
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //返回执行结果
            return result;
        }
        #endregion

        #region 单纯压缩字符串
        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="str">需要压缩的字符串</param>
        /// <returns>base64位字符串</returns>
        public string CompressStr(string str)
        {
            //判断字符串是否为空，为空则返回结果为空
            if (str == null)
            {
                return "";
            }
            //字符串不为空则执行一下语句
            try
            {
                //定义数组存放压缩字符串字节序列
                byte[] bytData = System.Text.Encoding.Unicode.GetBytes(str);
                //创建存储内存流
                MemoryStream ms = new MemoryStream();
                //实例化BZip2OutputStream类
                Stream s = new ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream(ms);
                //字节压缩
                s.Write(bytData, 0, bytData.Length);
                //关闭内存流，释放字节序列
                s.Close();
                //获取返回的字节序列数组
                byte[] compressedData = (byte[])ms.ToArray();
                //获取数组转化为base64位字符串
                string result = System.Convert.ToBase64String(compressedData, 0, compressedData.Length) + " " + bytData.Length;
                //返回base64位字符串
                return result;
            }
            catch (Exception ex)
            {
                //报错信息
                throw ex;
            }
        }
        #endregion

        #region 单纯解压缩字符串
        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="str">需要解压的base64位字符串</param>
        /// <returns>字符串</returns>
        public string UnCompressStr(string str)
        {
            //判断需解压字符串是否为空
            if (str == null)
            {
                //如果为空，返回空
                return "";
            }
            //如果不为空则执行以下语句
            try
            {
                //定义字符串数组，获取需解压字符串数据包
                string[] temp = str.Split(' ');
                //如果字符小于2返回空
                if (temp.Length < 2)
                {
                    return "";
                }
                //调用文本可变字符串类
                System.Text.StringBuilder uncompressedString = new System.Text.StringBuilder();
                int totalLength = 0;
                //base64位字符串数字转化为8位字符数组
                byte[] bytInput = System.Convert.FromBase64String(temp[0]);
                //转化为等效32位字符数组
                byte[] writeData = new byte[Convert.ToInt32(temp[1])];
                //构建读取流
                Stream s2 = new ICSharpCode.SharpZipLib.BZip2.BZip2InputStream(new MemoryStream(bytInput));
                //循环解压文本
                while (true)
                {
                    int size = s2.Read(writeData, 0, writeData.Length);
                    if (size > 0)
                    {
                        totalLength += size;
                        uncompressedString.Append(System.Text.Encoding.Unicode.GetString(writeData, 0, size));
                        //返回解压后文本
                        return uncompressedString.ToString();
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 是否安装RAR校验
        /// <summary>
        /// 是否安装了Winrar
        /// </summary>
        /// <returns>true为已安装，false为未安装</returns>
        public static bool Exists()
        {
            //从注册表中获取安装路径
            RegistryKey the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
            //判断是否存在可执行文件
            return !string.IsNullOrEmpty(the_Reg.GetValue("").ToString());
        }
        #endregion


        #region RAR压缩文件
        /// <summary>
        /// 打包成Rar
        /// </summary>
        /// <param name="patch">源文件地址</param>
        /// <param name="rarPatch">存放压缩包地址</param>
        /// <param name="rarName">生成压缩包名字</param>
        /// <returns>解压成功返回true，失败抛错，返回false</returns>
        public Boolean CompressRAR(string patch, string rarPatch, string rarName)
        {
            string the_rar;
            RegistryKey the_Reg;
            object the_Obj;
            string the_Info;
            ProcessStartInfo the_StartInfo;
            Process the_Process;
            bool Result = false;
            try
            {
                if (!Exists())
                {
                    return false;
                }
                the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                //the_rar = the_rar.Substring(1, the_rar.Length - 7);
                if (Directory.Exists(patch) == false)
                {
                    Directory.CreateDirectory(patch);
                }
                //命令参数
                //the_Info = " a    " + rarName + " " + patch + " -r"; 

                the_Info = @"a -ibck  -r  -ep1 " + rarName + "  " + patch;

                the_StartInfo = new ProcessStartInfo();
                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //打包文件存放目录
                the_StartInfo.WorkingDirectory = rarPatch;
                the_Process = new Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
                the_Process.WaitForExit();
                if (the_Process.HasExited)
                {
                    Result = true;
                }
                the_Process.Close();
                return Result;
            }
            catch (Exception ex)
            {
                return Result;
            }
        }



        #endregion

        #region ZIP压缩文件
        /// <summary>
        /// 打包成Rar
        /// </summary>
        /// <param name="patch">源文件地址</param>
        /// <param name="rarPatch">存放压缩包地址</param>
        /// <param name="rarName">生成压缩包名字</param>
        /// <returns>解压成功返回true，失败抛错</returns>
        /// <summary>
        /// 压缩文件
        /// </summary>
        public bool ZipFileMain(string patch, string rarPatch, string rarName)
        {
            string[] filenames = Directory.GetFiles(patch);
            //初始化结果为false
            bool result = false;
            try
            {
                Crc32 crc = new Crc32();
                //拼接压缩包返回路径
                string _FilePathOut = Path.Combine(rarPatch, rarName);
                //创建或覆盖指定存放路径
                ZipOutputStream s = new ZipOutputStream(File.Create(_FilePathOut));

                s.SetLevel(5);
                //循环压缩文件
                foreach (string file in filenames)
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry((new FileInfo(file)).Name);

                    entry.DateTime = DateTime.Now;

                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
                s.Finish();
                s.Close();
                //压缩完成后重置结果为true
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //返回执行结果
            return result;
        }
        #endregion
    }
}