using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    /// <summary>
    /// 数据缓存类
    /// </summary>
    public class DataCacheProvider
    {
        #region 数据缓存变量
        /// <summary>
        /// 数据缓存变量
        /// </summary>
        private static ConcurrentDictionary<string, object> _DataCache = new ConcurrentDictionary<string, object>();
        /// <summary>
        /// 数据缓存变量
        /// </summary>
        private static ConcurrentDictionary<string, object> DataCache { get { return _DataCache; } set { _DataCache = value; } }
        #endregion

        #region 设置缓存
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool SetCache(string Key, object Value)
        {
            try
            {
                string _Key = FormatKey(Key);
                DataCache.AddOrUpdate(_Key, Value, (TKey, TOldValue) =>
                {
                    return Value;
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        #endregion

        #region 获取数据缓存
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public object GetCache(string Key)
        {
            try
            {
                string _Key = FormatKey(Key);

                return DataCache[Key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <returns></returns>
        public bool ClearCache()
        {
            try
            {
                DataCache.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        #endregion

        #region 移除缓存
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool RemoveCache(string Key)
        {
            try
            {
                string _Key = FormatKey(Key);
                if (!DataCache.ContainsKey(_Key))
                {
                    return true;
                }
                object value = "";
                return DataCache.TryRemove(_Key, out value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 格式化键值
        /// <summary>
        /// 格式化键值
        /// </summary>
        /// <param name="Key">键值</param>
        /// <returns></returns>
        private string FormatKey(string Key)
        {
            string _Key = "";
            if (!string.IsNullOrEmpty(Key))
            {
                _Key = Key.Trim();
            }
            return _Key;
        }
        #endregion
    }
}
