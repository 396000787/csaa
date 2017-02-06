using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// 序列化操作接口
    /// </summary>
    interface ISerializeOperate
    {
        /// <summary>
        /// 序列化对象到字符串
        /// </summary>
        /// <param name="_Object">泛型对象</param>
        /// <returns>序列化后的字符串</returns>
        string Serialize(object _Object);
        /// <summary>
        /// 反序列化字符串到对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="ObjectString">要转换为对象的字符串</param>
        /// <returns>反序列化出来的对象</returns>
        T Desrialize<T>(string ObjectString);
        /// <summary>
        /// 把图片转换成Base64数字编码的等效字符串
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <returns>Base64数字编码的等效字符串</returns>
        string ImageToString(Image _Image);
        /// <summary>
        /// 把Base64数字编码的等效字符串转换成Image对象
        /// </summary>
        /// <param name="_ImageString">Base64数字编码的等效字符串</param>
        /// <returns>图片对象</returns>
        Image StringToImage(string _ImageString);
        /// <summary>
        /// 根据图片路径返回Base64数字编码的等效字符串
        /// </summary>
        /// <param name="_ImagePath">图片路径</param>
        /// <returns>Base64数字编码的等效字符串</returns>
        string ImageToStringByPath(string _ImagePath);
        /// <summary>
        /// 根据Base64数字编码的等效字符串在指定位置创建图片
        /// </summary>
        /// <param name="_ImageString">Base64数字编码的等效字符串</param>
        /// <param name="_ImagePath">图片生成路径（包括图片名和扩展名）</param>
        /// <returns>是否成功，是：成功；否：不成功。</returns>
        bool ImageToFileByString(string _ImageString, string _ImagePath);
    }
}
