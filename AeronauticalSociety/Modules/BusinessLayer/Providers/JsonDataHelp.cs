using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.iTools;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    public class JsonDataHelp
    {
        #region json数据文件存放路径
        /// <summary>
        /// json数据文件存放路径
        /// </summary>
        public string JsonFileDir { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData"); } }
        #endregion

        #region 加载json数据
        /// <summary>
        /// 加载json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public T GetJsonData<T>(string FilePath)
        {
            T ResultItem = Activator.CreateInstance<T>();
            string result = LoadJsonStr(FilePath);
            if (string.IsNullOrEmpty(result))
            {
                return ResultItem;
            }
            ResultItem = result.FormJsonString<T>();
            return ResultItem;
        }
        #endregion

        #region 读取json文件
        /// <summary>
        /// 读取json文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public string LoadJsonStr(string FilePath)
        {
            string result = "";
            //生成文件路径
            string JsonFilePath = Path.Combine(this.JsonFileDir, FilePath);
            //判断文件是否存在
            if (!File.Exists(JsonFilePath))
            {
                return result;
            }
            result = File.ReadAllText(JsonFilePath, Encoding.UTF8);
            return result;
        }
        #endregion
    }
}
