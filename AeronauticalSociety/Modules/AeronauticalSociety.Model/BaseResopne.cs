using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AeronauticalSociety.Model
{
    public class BaseResopne<T>
    {
        #region 执行是否成功
        /// <summary>
        /// 执行是否成功
        /// </summary>
        private bool _IsSuccess = true;
        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool IsSuccess { get { return _IsSuccess; } set { _IsSuccess = value; } }
        #endregion

        #region 错误信息
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; set; }
        #endregion

        #region 数据总数
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; }
        #endregion

        #region 数据结果
        /// <summary>
        /// 数据结果
        /// </summary>
        private T _Data = Activator.CreateInstance<T>();
        /// <summary>
        /// 数据结果
        /// </summary>
        public T Data { get { return _Data; } set { _Data = value; } }
        #endregion
    }
}
