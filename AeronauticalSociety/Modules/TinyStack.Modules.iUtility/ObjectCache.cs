using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Collections;

namespace TinyStack.Modules.iUtility
{
    /// <summary>
    /// 对象缓存器。
    /// </summary>
    /// <typeparam name="TKey">缓存键类型。</typeparam>
    /// <typeparam name="TValue">缓存对象值类型。</typeparam>
    public class ObjectCache<TKey, TValue>
    {
        #region 内部字典实例

        /// <summary>
        /// 内部字典实例。
        /// </summary>
        private ConcurrentDictionary<TKey, TValue> _innerCache;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法。
        /// </summary>
        public ObjectCache()
        {
            _innerCache = new ConcurrentDictionary<TKey, TValue>();
        }

        #endregion

        #region 实例属性

        /// <summary>
        /// 当前对象缓存实例中包含的对象数目。
        /// </summary>
        public int Count
        {
            get
            {
                return _innerCache.Count;
            }
        }

        /// <summary>
        /// 当前对象缓存实例是否不包含任何对象。
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return _innerCache.IsEmpty;
            }
        }

        #endregion

        #region 实例方法

        /// <summary>
        /// 缓存中是否包含指定对象。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <returns>是否包含指定的对象。</returns>
        public bool ContainsKey(TKey key)
        {
            return _innerCache.ContainsKey(key);
        }

        /// <summary>
        /// 删除缓存中的所有对象。
        /// 暂时不公开，仅用于测试。
        /// </summary>
        internal void Clear()
        {
            // HACK: 不公开遍历和清空方法，如何回收资源？
            _innerCache.Clear();
        }

        /// <summary>
        /// 添加或更新指定的缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="addValue">要添加的对象。</param>
        /// <param name="updateValueFactory">创建要更新的对象的方法。</param>
        /// <returns>添加或更新后，指定缓存的值。</returns>
        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            return _innerCache.AddOrUpdate(key, addValue, updateValueFactory);
        }

        /// <summary>
        /// 添加或更新指定的缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="addValueFactory">创建要添加的对象的方法。</param>
        /// <param name="updateValueFactory">创建要更新的对象的方法。</param>
        /// <returns>添加或更新后，指定缓存的值。</returns>
        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            return _innerCache.AddOrUpdate(key, addValueFactory, updateValueFactory);
        }

        /// <summary>
        /// 获取或添加指定的缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">要添加的对象。</param>
        /// <returns>获取或添加后，指定缓存的值。</returns>
        public TValue GetOrAdd(TKey key, TValue value)
        {
            return _innerCache.GetOrAdd(key, value);
        }

        /// <summary>
        /// 获取或添加指定的缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="valueFactory">创建要添加的对象的方法。</param>
        /// <returns>获取或添加后，指定缓存的值。</returns>
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            return _innerCache.GetOrAdd(key, valueFactory);
        }

        /// <summary>
        /// 尝试添加一个对象到缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">缓存对象。</param>
        /// <returns>如果当前缓存中已经包含指定的缓存键，则返回false，否则返回true。</returns>
        public bool TryAdd(TKey key, TValue value)
        {
            return _innerCache.TryAdd(key, value);
        }

        /// <summary>
        /// 尝试从缓存中删除一个对象。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">缓存对象。</param>
        /// <returns>如果当前缓存中已经包含指定的缓存键，则返回true，否则返回false。</returns>
        public bool TryRemove(TKey key, out TValue value)
        {
            return _innerCache.TryRemove(key, out value);
        }

        /// <summary>
        /// 尝试获取一个缓存的值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">缓存对象。</param>
        /// <returns>如果当前缓存中已经包含指定的缓存键，则返回false，否则返回true。</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _innerCache.TryGetValue(key, out value);
        }

        /// <summary>
        /// 尝试更新一个缓存的值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="newValue">要更新的对象。</param>
        /// <param name="comparisonValue">用于比较判断是否要更新的值。</param>
        /// <returns>如果当前缓存中已经包含指定的缓存键，则返回false，否则返回true。</returns>
        public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
        {
            return _innerCache.TryUpdate(key, newValue, comparisonValue);
        }

        #endregion
    }
}
