using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// Json操作接口
    /// </summary>
    interface IJsonOperate
    {
        /// <summary>
        /// 将对象(集合)转换成Json字符串
        /// </summary>
        /// <param name="obj">对象(集合)</param>
        /// <returns>Json字符串</returns>
        string ToJsonString(object obj);

        /// <summary>
        /// 将Json字符串转换成对象(集合)
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>泛型</returns>
        T FromJsonString<T>(string jsonString);

        /// <summary>
        /// 将xml文本转换成Json字符串
        /// </summary>
        /// <param name="xmlDoc">xml文本</param>
        /// <returns>Json字符串</returns>
        string XmlToJson(XmlNode node);

        /// <summary>
        /// 将Json字符串转换成xml文本
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>xml文本</returns>
        XmlDocument JsonToXml(string jsonString);

    }
}
