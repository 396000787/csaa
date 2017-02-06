using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TinyStack.Modules.iUtility
{
    public class iCache
    {
        #region 私有变量

        /// <summary>
        /// 新建一个缓存对象
        /// </summary>
        private MyCache myCache_ = new MyCache();

        #endregion

        #region 公开方法

        /// <summary>
        /// 创建存储空间
        /// </summary>
        /// <returns>新开辟存储空间</returns>
        public MyCache creater()
        {
            MyCache iMyCache = new MyCache();
            return iMyCache;
        }

        /// <summary>
        /// 将缓存中的key锁定，不能对key进行修改或删除
        /// </summary>
        /// <param name="key">缓存要被锁定的key</param>
        /// <returns>解锁的密钥</returns>
        public Guid lockKey(string key)
        {
            return myCache_.lockKey(key);
        }

        /// <summary>
        /// 将锁定的key进行解锁，解锁后可以对key进行修改或删除
        /// </summary>
        /// <param name="lockGuid">解锁的密钥</param>
        /// <param name="key">缓存被锁的key</param>
        /// <returns>false:解锁失败,true:解锁成功</returns>
        public Boolean unLockKey(string key, Guid lockGuid)
        {
            return myCache_.unLockKey(key, lockGuid);
        }

        /// <summary>
        /// 存储缓存数据到公共存储空间
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <param name="val">缓存的value</param>
        /// <returns>false:存值失败,true:存值成功</returns>
        public Boolean setValue(string key, Object val)
        {
            return myCache_.setValue(key, val);
        }

        /// <summary>
        /// 对缓存进行安全存储
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <param name="val">缓存的value</param>
        /// <param name="isSafety">是否设置安全锁</param>
        /// <returns>权限密钥</returns>
        public Guid setValue(string key, Object val, Boolean isSafety)
        {
            return myCache_.setValue(key, val, isSafety);
        }

        /// <summary>
        /// 读取缓存中的值
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <returns>缓存的value</returns>
        public Object getValue(string key)
        {
            return myCache_.getValue(key);
        }

        /// <summary>
        /// 对缓存进行安读取数据，需要GUID
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <param name="GUID">权限密钥</param>
        /// <returns>缓存的value</returns>
        public Object getValue(string key, Guid keyGuid)
        {
            return myCache_.getValue(key, keyGuid);
        }

        /// <summary>
        /// 对缓存进行更新
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <param name="val">缓存的value</param>
        /// <returns>false:更新失败,true:更新成功</returns>
        public Boolean updateValue(string key, Object val)
        {
            return myCache_.updateValue(key, val);
        }

        /// <summary>
        /// 对缓存进行安全更新，需要GUID
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <param name="val">缓存的value</param>
        /// <param name="keyGuid">权限密钥</param>
        /// <returns>false:更新失败,true:更新成功</returns>
        public Boolean updateValue(string key, Object val, Guid keyGuid)
        {
            return myCache_.updateValue(key, val, keyGuid);
        }

        /// <summary>
        /// 对缓存进行一般删除
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <returns>false:删除失败,true:删除成功</returns>
        public Boolean removeValue(string key)
        {
            return myCache_.removeValue(key);
        }

        /// <summary>
        /// 对缓存进行安全删除
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <param name="GUID"></param>
        /// <returns>false:删除失败,true:删除成功</returns>
        public Boolean removeValue(string key, Guid keyGuid)
        {
            return myCache_.removeValue(key, keyGuid);
        }
        /// <summary>
        /// 手动回收资源
        /// </summary>
        public void Disposed()
        {
            myCache_.Disposed();
        }
        #endregion

        #region 存储空间
        /// <summary>
        /// 存储空间
        /// </summary>
        public class MyCache
        {
            /// <summary>
            /// 缓存(安全存储)
            /// </summary>
            private Hashtable iCahceHashtable_safe = Hashtable.Synchronized(new Hashtable());
            /// <summary>
            /// 缓存(非安全存储)
            /// </summary>
            private Hashtable iCahceHashtable = Hashtable.Synchronized(new Hashtable());

            /// <summary>
            /// 记录缓存需要锁定的key
            /// </summary>
            private Hashtable lockKeyHashtable = new Hashtable();

            /// <summary>
            /// 将缓存中的key锁定，不能对key进行修改或删除
            /// </summary>
            /// <param name="key">缓存要被锁定的key</param>
            /// <returns>解锁的密钥</returns>
            public Guid lockKey(string key)
            {
                Guid lockGUID = Guid.NewGuid();
                try
                {
                    // 判断lockTable中是否已经锁定了key
                    if (lockKeyHashtable.ContainsValue(key))
                    {
                        //Guid的值不能为空,人为制造一个Guid返回,无他用.
                        return Guid.NewGuid();
                    }

                    lockKeyHashtable.Add(lockGUID, key);
                }
                catch (Exception Err)
                {
                }
                return lockGUID;
            }

            /// <summary>
            /// 将锁定的key进行解锁，解锁后可以对key进行修改或删除
            /// </summary>
            /// <param name="lockGUID">解锁的密钥</param>
            /// <param name="key">缓存被锁的key</param>
            /// <returns>false:解锁失败,true:解锁成功</returns>
            public Boolean unLockKey(string key, Guid lockGUID)
            {
                try
                {
                    if (!lockKeyHashtable.ContainsValue(key))// 判断lockTable是否含有key
                    {
                        return false;
                    }
                    if (!lockKeyHashtable.ContainsKey(lockGUID))// 判断lockTable中是否含有lockGUID
                    {
                        return false;
                    }
                    lockKeyHashtable.Remove(lockGUID);
                    return true;
                }
                catch (Exception Err)
                {
                }
                return false;
            }


            /// <summary>
            /// 对缓存进行写入(非安全存储)
            /// </summary>
            /// <param name="key">真实的key</param>
            /// <param name="val">真实的value</param>
            /// <returns>false:写入缓存失败,true:成功</returns>
            public Boolean setValue(string key, Object val)
            {
                try
                {
                    if (!iCahceHashtable.ContainsKey(key))  // 判断myth是否已经含有key
                    {
                        KeyValue tableValue = new KeyValue(key, val);
                        iCahceHashtable.Add(key, tableValue);
                        return true;
                    }
                }
                catch (Exception e)
                {
                }
                return false;
            }
            /// <summary>
            /// 对缓存进行写入(安全存储)
            /// </summary>
            /// <param name="key">缓存的key</param>
            /// <param name="val">缓存的value</param>
            /// <param name="isSafety">是否设置安全锁</param>
            /// <returns>权限密钥</returns>
            public Guid setValue(string key, Object val, Boolean isSafety)
            {
                //lock (iCahceHashtable)
                try
                {
                    // 判断是否安全存储 判断缓存是否含有key
                    if (isSafety && !iCahceHashtable_safe.ContainsKey(key))
                    {
                        KeyValue tableValue = new KeyValue(key, val);
                        iCahceHashtable_safe.Add(key, tableValue);
                        return tableValue.KeyGuid;
                    }
                    setValue(key, val);
                }
                catch (Exception e)
                {
                    //Guid的值不能为空,人为制造一个Guid返回,无他用.
                    return Guid.NewGuid();
                }
                //Guid的值不能为空,人为制造一个Guid返回,无他用.
                return Guid.NewGuid();
            }

            /// <summary>
            /// 对缓存进行读取(非安全存储)
            /// </summary>
            /// <param name="key">真实的key</param>
            /// <returns>真实的value</returns>
            public Object getValue(string key)
            {
                bool lockWasSuccessful = false;
                KeyValue keyValue = null;
                while (true)
                {
                    if (iCahceHashtable.ContainsKey(key))// 判断key是否在myht中存在
                    {
                        lock (iCahceHashtable)
                        {
                            try
                            {
                                keyValue = (KeyValue)iCahceHashtable[key];
                                lockWasSuccessful = System.Threading.Monitor.TryEnter(keyValue);
                            }
                            catch (Exception Err)
                            {
                                return null;
                            }

                        }
                        if (lockWasSuccessful == false)
                        {
                            System.Threading.Thread.Sleep(0);
                            continue;
                        }

                        // 如果运行到这里说明对象keyValue已经被锁定
                        try
                        {
                            return keyValue.Value;
                        }
                        finally
                        {
                            System.Threading.Monitor.Exit(keyValue);
                        }

                    }
                    break;
                }
                return null;
            }

            /// <summary>
            /// 对缓存进行读取(安全存储)
            /// </summary>
            /// <param name="key">真实的key</param>
            /// <returns>真实的value</returns>
            public Object getValue(string key, Guid KeyGUID)
            {
                bool lockWasSuccessful = false;
                KeyValue keyValue = null;
                while (true)
                {
                    // 判断key是否在缓存中存在,权限密钥是否一致
                    if (iCahceHashtable_safe.ContainsKey(key)
                        && KeyGUID.Equals(((KeyValue)iCahceHashtable_safe[key]).KeyGuid))
                    {
                        lock (iCahceHashtable_safe)
                        {
                            try
                            {
                                keyValue = (KeyValue)iCahceHashtable_safe[key];
                                lockWasSuccessful = System.Threading.Monitor.TryEnter(keyValue);
                            }
                            catch (Exception Err)
                            {
                                return null;
                            }

                        }
                        if (lockWasSuccessful == false)
                        {
                            System.Threading.Thread.Sleep(0);
                            continue;
                        }

                        // 如果运行到这里说明对象keyValue已经被锁定
                        try
                        {
                            return keyValue.Value;
                        }
                        finally
                        {
                            System.Threading.Monitor.Exit(keyValue);
                        }

                    }
                    break;
                }
                return null;
            }

            /// <summary>
            /// 对缓存进行更新(非安全存储)
            /// </summary>
            /// <param name="key">真实的key</param>
            /// <param name="val">真实的value</param>
            /// <returns>false:更新失败,true:成功</returns>
            public Boolean updateValue(string key, Object val)
            {
                try
                {
                    //  判断key是否被缓存锁锁定,key在公共缓存中是否存在
                    if (lockKeyHashtable.ContainsValue(key)
                        || !iCahceHashtable.ContainsKey(key))
                    {
                        return false;
                    }
                    ((KeyValue)iCahceHashtable[key]).Value = val;
                    return true;
                }
                catch (Exception Err)
                {
                }
                return false;
            }

            /// <summary>
            /// 对缓存进行更新(安全存储)
            /// </summary>
            /// <param name="key">真实的key</param>
            /// <param name="val">真实的value</param>
            /// <returns>false:更新失败,true:成功</returns>
            public Boolean updateValue(string key, Object val, Guid KeyGUID)
            {
                try
                {
                    // 判断key是否被缓存锁锁定,key在公共缓存中是否存在,权限密钥是否一致
                    if (lockKeyHashtable.ContainsValue(key)
                        || !iCahceHashtable_safe.ContainsKey(key)
                        || !KeyGUID.Equals(((KeyValue)iCahceHashtable_safe[key]).KeyGuid))
                    {
                        return false;
                    }
                    ((KeyValue)iCahceHashtable_safe[key]).Value = val;
                    return true;
                }
                catch (Exception Err)
                {
                }
                return false;
            }

            /// <summary>
            /// 对缓存进行删除
            /// </summary>
            /// <param name="key">真实的key</param>
            /// <returns>false:删除失败,true:成功</returns>
            public Boolean removeValue(string key)
            {
                try
                {
                    // 判断key是否被缓存锁锁定,key在公共缓存中是否存在
                    if (lockKeyHashtable.ContainsValue(key)
                        || !iCahceHashtable.ContainsKey(key))
                    {
                        return false;
                    }
                    iCahceHashtable.Remove(key);
                    return true;
                }
                catch (Exception Err)
                {
                }
                return false;
            }
            /// <summary>
            /// 对缓存进行删除
            /// </summary>
            /// <param name="key">真实的key</param>
            /// <returns>false:删除失败,true:成功</returns>
            public Boolean removeValue(string key, Guid KeyGUID)
            {
                try
                {
                    // 判断key是否被缓存锁锁定,key在公共缓存中是否存在,权限密钥是否一致
                    if (lockKeyHashtable.ContainsValue(key)
                        || !iCahceHashtable_safe.ContainsKey(key)
                        || !KeyGUID.Equals(((KeyValue)iCahceHashtable_safe[key]).KeyGuid))
                    {
                        return false;
                    }
                    iCahceHashtable_safe.Remove(key);
                    return true;
                }
                catch (Exception Err)
                {
                }
                return false;
            }
            /// <summary>
            /// 手动回收资源
            /// </summary>
            public void Disposed()
            {
                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                catch (Exception Err)
                {
                }
            }
        }
        #endregion

        #region 缓存中的存储类，保存了最基本的key和value

        /// <summary>
        /// 缓存中的存储类，保存了最基本的key和value
        /// </summary>
        private class KeyValue
        {
            /// <summary>
            /// 类KeyValue的构造方法
            /// </summary>
            /// <param name="key"></param>
            /// <param name="val"></param>
            public KeyValue(string key, Object val)
            {
                this.Key = key;
                this.Value = val;
            }
            /// <summary>
            /// 属性keyGuid,权限密钥
            /// </summary>
            private Guid keyGuid = Guid.NewGuid();

            /// <summary>
            /// 属性keyGuid的get方法
            /// </summary>
            public Guid KeyGuid
            {
                get { return keyGuid; }
            }

            /// <summary>
            /// 属性Key,记录缓存的真实key
            /// </summary>
            private string key = null;

            /// <summary>
            /// 属性key的set和get方法
            /// </summary>
            public string Key
            {
                get { return key; }
                set { key = value; }
            }

            /// <summary>
            /// 属性value,记录缓存的真实value
            /// </summary>
            private Object value = null;

            /// <summary>
            /// 属性value的set和get方法
            /// </summary>
            public Object Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }
        #endregion
    }
}
