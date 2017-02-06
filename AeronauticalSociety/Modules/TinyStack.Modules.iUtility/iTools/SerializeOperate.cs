using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.InterFaceUtility;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TinyStack.Modules.iUtility.iTools
{
    /// <summary>
    /// 序列化与反序列化
    /// </summary>
    public class SerializeOperate : ISerializeOperate
    {

        #region 序列化对象到字符串
        /// <summary>
        /// 序列化对象到字符串(序列化类需要添加[Serializable]属性标记类，可以序列化DataSet)
        /// </summary>
        /// <param name="_Object">泛型对象</param>
        /// <returns>序列化后的字符串</returns>
        public string Serialize(object _Object)
        {
            try
            {
                //实例化序列化操作类
                IFormatter _IFormatter = new BinaryFormatter();
                //实例化内存流操作类
                MemoryStream _MemoryStream = new MemoryStream();
                //将对象或具有给定根的对象图形序列化为所提供的流
                _IFormatter.Serialize(_MemoryStream, _Object);
                //设置流中的当前位置
                _MemoryStream.Position = 0;
                //声明一个字节数组，将流转换为字节数组
                Byte[] _Byte = new Byte[_MemoryStream.Length];
                //从当前流中读取字节块并将数据写入字节数组中
                _MemoryStream.Read(_Byte, 0, _Byte.Length);
                //重写Stream.Flush以便不执行任何操作
                _MemoryStream.Flush();
                //关闭当前流并释放与之关联的所有资源
                _MemoryStream.Close();
                //将8位无符号整数的数组转换为其用Base64数字编码的等效字符串表示形式，并返回
                return Convert.ToBase64String(_Byte);
            }
            catch (Exception ex)
            {
                //抛出错误信息
                throw new Exception("序列化失败,原因:" + ex.Message);
            }
        }
        #endregion

        #region 反序列化字符串到对象
        /// <summary>
        /// 反序列化字符串到对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="ObjectString">要转换为对象的字符串</param>
        /// <returns>反序列化出来的对象</returns>
        public T Desrialize<T>(string ObjectString)
        {
            T _Object = default(T);
            try
            {
                //实例化序列化操作类
                IFormatter _IFormatter = new BinaryFormatter();
                //将指定的字符串（它将二进制数据编码为 Base64 数字）转换为等效的 8 位无符号整数数组。
                Byte[] _Byte = Convert.FromBase64String(ObjectString);
                //将字节数组初始化为流
                MemoryStream stream = new MemoryStream(_Byte);
                //反序列化所提供流中的数据并重新组成指定对象
                _Object = (T)_IFormatter.Deserialize(stream);
                //重写Stream.Flush以便不执行任何操作
                stream.Flush();
                //关闭当前流并释放与之关联的所有资源
                stream.Close();
            }
            catch (Exception ex)
            {
                //抛出错误信息
                throw new Exception("反序列化失败,原因:" + ex.Message);
            }
            //返回重新组成的对象
            return _Object;
        }
        #endregion

        #region 把图片转换成Base64数字编码的等效字符串
        /// <summary>
        /// 把图片转换成Base64数字编码的等效字符串
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <returns>Base64数字编码的等效字符串</returns>
        public string ImageToString(Image _Image)
        {
            string _ImageString = "";
            try
            {
                //实例化流操作类
                MemoryStream _MemoryStream = new MemoryStream();
                //将此图像以Jpeg格式保存在流中
                _Image.Save(_MemoryStream, ImageFormat.Jpeg);
                //创建此流的无符号字节数组
                Byte[] _ImageByte = _MemoryStream.GetBuffer();
                //将8位无符号整数的数组转换为其用Base64数字编码的等效字符串表示形式
                _ImageString = Convert.ToBase64String(_ImageByte);
            }
            catch (Exception ex)
            {
                //抛出错误信息
                throw new Exception("图片转Base64字符串失败,原因:" + ex.Message);
            }
            //返回Base64数字编码的等效字符串
            return _ImageString;  
        }
        #endregion

        #region 把Base64数字编码的等效字符串转换成Image对象
        /// <summary>
        /// 把Base64数字编码的等效字符串转换成Image对象
        /// </summary>
        /// <param name="_ImageString">Base64数字编码的等效字符串</param>
        /// <returns>图片对象</returns>
        public Image StringToImage(string _ImageString)
        {
            Image _Image;
            try
            {
                //判断传入字符串是否为空
                if (string.IsNullOrEmpty(_ImageString))
                {
                    //如果为空，返回null
                    return null;
                }
                //将指定的字符串（它将二进制数据编码为 Base64 数字）转换为等效的 8 位无符号整数数组。
                Byte[] _ImageByte = Convert.FromBase64String(_ImageString);
                //将字节数组初始化为流
                MemoryStream ms = new MemoryStream(_ImageByte);
                //从指定的流创建图像
                _Image = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                //抛出错误信息
                throw new Exception("Base64字符串转图片失败,原因:" + ex.Message);
            }
            return _Image;
        }
        #endregion

        #region 根据图片路径返回Base64数字编码的等效字符串
        /// <summary>
        /// 根据图片路径返回Base64数字编码的等效字符串
        /// </summary>
        /// <param name="_ImagePath">图片路径</param>
        /// <returns>Base64数字编码的等效字符串</returns>
        public string ImageToStringByPath(string _ImagePath)
        {
            string _ImageString = "";
            try
            {
                //判断指定路径的图片是否存在
                if (!File.Exists(_ImagePath))
                {
                    //如果不存在，返回null
                    return null;
                }
                //获取指定路径字符串的扩展名的小写形式
                string _ImageExtension = Path.GetExtension(_ImagePath).ToLower();
                //判断扩展名是否为图片
                if (_ImageExtension == ".bmp" || _ImageExtension == ".gif" || _ImageExtension == ".ico" || _ImageExtension == ".jpeg" || _ImageExtension == ".jpg" || _ImageExtension == ".png")
                {
                    Image _Image = Image.FromFile(_ImagePath);
                    Image _bmp = new Bitmap(_Image);
                    _Image.Dispose();
                    //实例化流操作类
                    MemoryStream _MemoryStream = new MemoryStream();
                    //将此图像以Jpeg格式保存在流中
                    _bmp.Save(_MemoryStream, ImageFormat.Jpeg);
                    //创建此流的无符号字节数组
                    Byte[] _ImageByte = _MemoryStream.GetBuffer();
                    //将8位无符号整数的数组转换为其用Base64数字编码的等效字符串表示形式
                    _ImageString = Convert.ToBase64String(_ImageByte);
                }
                else
                {
                    //如果不是图片，返回null
                    return null;
                }
            }
            catch (Exception ex)
            {
                //抛出错误信息
                throw new Exception("根据图片路径返回Base64数字编码的等效字符串失败,原因:" + ex.Message);
            }
            return _ImageString;
        }
        #endregion

        #region 根据Base64数字编码的等效字符串在指定位置创建图片
        /// <summary>
        /// 根据Base64数字编码的等效字符串在指定位置创建图片
        /// </summary>
        /// <param name="_ImageString">Base64数字编码的等效字符串</param>
        /// <param name="_ImagePath">图片生成路径（包括图片名和扩展名）</param>
        /// <returns>是否成功，是：成功；否：不成功。</returns>
        public bool ImageToFileByString(string _ImageString, string _ImagePath)
        {
            bool _IsSuccess = false;
            try
            {
                //判断传入字符串和图片生成路径是否为空
                if (string.IsNullOrEmpty(_ImageString) || string.IsNullOrEmpty(_ImagePath))
                {
                    //如果为空，返回false
                    return _IsSuccess;
                }
                //获取指定路径字符串的扩展名的小写形式
                string _ImageExtension = Path.GetExtension(_ImagePath).ToLower();
                //判断扩展名是否为图片
                if (_ImageExtension == ".bmp" || _ImageExtension == ".gif" || _ImageExtension == ".ico" || _ImageExtension == ".jpeg" || _ImageExtension == ".jpg" || _ImageExtension == ".png")
                {
                    //将指定的字符串（它将二进制数据编码为 Base64 数字）转换为等效的 8 位无符号整数数组。
                    Byte[] _ImageByte = Convert.FromBase64String(_ImageString);
                    //获取生成图片路径的目录信息
                    string _Root = Path.GetDirectoryName(_ImagePath);
                    //判断目录是否存在
                    if (!Directory.Exists(_Root))
                    {
                        //如果不存在，则创建目录
                        Directory.CreateDirectory(_Root);
                    }
                    //在指定路径中创建图片
                    FileStream fs = File.Create(_ImagePath);
                    //通过filestream对象往文件中写入图片信息
                    fs.Write(_ImageByte, 0, _ImageByte.Length);
                    //关闭当前流并释放与之关联的所有资源
                    fs.Close();
                    //成功设置为true
                    _IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                //抛出错误信息
                throw new Exception("根据Base64数字编码的等效字符串在指定位置创建图片失败,原因:" + ex.Message);
            }
            //返回是否成功
            return _IsSuccess;
        }
        #endregion
    }
}
