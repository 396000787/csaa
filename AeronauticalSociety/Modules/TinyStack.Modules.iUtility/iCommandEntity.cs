using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.iTools;

namespace TinyStack.Modules.iUtility
{
    /// <summary>
    /// command数据模型定义类
    /// </summary>
    public class iCommandEntity
    {

        #region 命令索引信息
        /// <summary>
        /// 命令索引信息
        /// </summary>
        private Guid guid = Guid.NewGuid();
        /// <summary>
        /// 命令索引信息
        /// </summary>
        public Guid Guid
        {
            get { return guid; }
            set
            {
                if (value == new Guid())
                {
                    guid = Guid.NewGuid();
                    return;
                }
                guid = value;
            }
        }
        #endregion

        #region 虚拟服务名称(服务名+方法名)
        /// <summary>
        /// 虚拟服务名称(服务名+方法名)
        /// </summary>
        private String serviceName = "";
        /// <summary>
        /// 虚拟服务名称(服务名+方法名)
        /// </summary>
        public String ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; }
        }
        #endregion

        #region 服务数据(用于支持服务执行的必要请求数据信息)
        /// <summary>
        /// 服务数据(用于支持服务执行的必要请求数据信息)
        /// </summary>
        private Dictionary<String, String> requestData = new Dictionary<string, String>();
        /// <summary>
        /// 服务数据(用于支持服务执行的必要请求数据信息)
        /// </summary>
        public Dictionary<String, String> RequestData
        {
            get { return requestData; }
            set { requestData = value; }
        }
        #endregion

        #region 添加请求数据
        /// <summary>
        /// 添加请求数据
        /// </summary>
        /// <param name="parameterName">请求方法的参数关键字</param>
        /// <param name="parameterValue">与参数关键对应数据</param>
        public void AddRequestData(String parameterName, Object parameterValue)
        {
            try
            {
                JsonOperate _JsonOperate = new JsonOperate();
                requestData.Add(parameterName, _JsonOperate.ToJsonString(parameterValue));
            }
            catch
            {
            }
        }
        #endregion

        #region 服务返回数据
        /// <summary>
        /// 服务返回数据
        /// </summary>
        private String responseData = "";
        /// <summary>
        /// 服务返回数据
        /// </summary>
        public String ResponseData
        {
            get { return responseData; }
            set { responseData = value; }
        }
        #endregion

        #region 获取返回结果的反序列化结果
        /// <summary>
        /// 获取返回结果的反序列化结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetResponseData<T>()
        {
            JsonOperate _JsonOperate = new JsonOperate();
            return _JsonOperate.FromJsonString<T>(responseData);
        }
        #endregion

        #region 发送数据Old
        /// <summary>
        /// 发送数据Old
        /// </summary>
        public string PostData
        {
            get;
            set;
        }
        #endregion

        #region 客户端标识
        /// <summary>
        /// 客户端标识
        /// </summary>
        public Guid ClientGuid
        {
            get;
            set;
        }
        #endregion       

        #region 发送时间
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime PostDate
        {
            get;
            set;
        }
        #endregion

        #region 返回数据Old
        /// <summary>
        /// 返回数据Old
        /// </summary>
        public string ReturnData
        {
            get;
            set;
        }
        #endregion

        #region 返回时间
        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime ReturnDate
        {
            get;
            set;
        }
        #endregion

        #region  当前包索引
        /// <summary>
        /// 当前包索引
        /// </summary>
        public int PackageIndex
        {
            get;
            set;
        }
        #endregion

        #region 数据包总数
        /// <summary>
        /// 数据包总数
        /// </summary>
        private int _PackageCount = 1;
        /// <summary>
        /// 数据包总数
        /// </summary>
        public int PackageCount
        {
            get { return _PackageCount; }
            set { _PackageCount = value; }
        }
        #endregion
    }
}
