using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.InterFaceUtility;
using System.Collections;
using Newtonsoft.Json;
using System.Xml;


namespace TinyStack.Modules.iUtility.iTools
{
    public class JsonOperate : IJsonOperate
    {
        #region 将对象(集合)转换成Json字符串
        /// <summary>
        /// 将对象(集合)转换成Json字符串
        /// </summary>
        /// <param name="obj">对象(集合)</param>
        /// <returns>Json字符串</returns>
        private string ToJsonString(object obj, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(obj, converters);
        }
        #endregion

        #region 将Json字符串转换成对象(集合)
        /// <summary>
        /// 将Json字符串转换成对象(集合)
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>泛型</returns>
        public T FromJsonString<T>(string jsonString)
        {

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        #endregion

        #region 将xml文本转换成Json字符串
        /// <summary>
        /// 将xml文本转换成Json字符串
        /// </summary>
        /// <param name="node">xml文本</param>
        /// <returns>Json字符串</returns>
        public string XmlToJson(XmlNode node)
        {
            return JsonConvert.SerializeXmlNode(node);
        }
        #endregion

        #region 将Json字符串转换成xml文本
        /// <summary>
        /// 将Json字符串转换成xml文本
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>xml文本</returns>
        public XmlDocument JsonToXml(string jsonString)
        {
            return JsonConvert.DeserializeXmlNode(jsonString);
        }
        #endregion


        public string ToJsonString(object obj)
        {
            //throw new NotImplementedException();
            return ToJsonString(obj, null);
        }
    }
}
