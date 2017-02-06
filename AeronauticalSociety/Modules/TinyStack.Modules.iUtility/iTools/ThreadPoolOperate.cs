using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.InterFaceUtility;
using System.Threading;

namespace TinyStack.Modules.iUtility.iTools
{
    /// <summary>
    /// 线程池操作类
    /// </summary>
    public class ThreadPoolOperate : IThreadPoolOperate
    {
        #region 添加执行线程
        /// <summary>
        /// 添加执行线程
        /// </summary>
        /// <param name="callback">要执行的方法</param>
        /// <param name="state">方法的参数</param>
        /// <returns>添加结果</returns>
        public bool AddThread(WaitCallback callback, object state = null)
        {
            if (state == null)
            {
                return ThreadPool.QueueUserWorkItem(callback);
            }
            return ThreadPool.QueueUserWorkItem(callback, state);
        }
        #endregion

        #region 获取线程池剩余线程数
        /// <summary>
        /// 获取线程池剩余线程数
        /// </summary>
        /// <returns>线程池剩余线程数</returns>
        public int GetUnusedThread()
        {
            int work = 0;
            int cpt = 0;
            ThreadPool.GetAvailableThreads(out work, out cpt);
            return work;
        }
        #endregion

        #region 获取线程池最大线程数
        /// <summary>
        /// 获取线程池最大线程数
        /// </summary>
        /// <returns>线程池最大线程数</returns>
        public int GetMaxThreads() 
        {
            int maxThreadNum, portThreadNum;
            //获取线程池最大线程数
            ThreadPool.GetMaxThreads(out maxThreadNum, out portThreadNum);
            return maxThreadNum;
        }
        #endregion

        #region 获取最小空闲线程数
        /// <summary>
        /// 获取最小空闲线程数
        /// </summary>
        /// <returns>最小空闲线程数</returns>
        public int GetMinThreads()
        {
            int minThreadNum, portThreadNum;
            //获取最小空闲线程数
            ThreadPool.GetMinThreads(out minThreadNum, out portThreadNum);
            return minThreadNum;
        }
        #endregion

        #region 设置线程池最大线程数
        /// <summary>
        /// 设置线程池最大线程数
        /// </summary>
        /// <param name="maxThreadNum">最大线程数</param>
        /// <returns>设置结果</returns>
        public bool SetMaxThreads(int maxThreadNum)
        {
            return ThreadPool.SetMaxThreads(maxThreadNum, maxThreadNum);
        }
        #endregion

        #region 设置线程池空闲数
        /// <summary>
        /// 设置线程池空闲数
        /// </summary>
        /// <param name="minThreadNum">空闲数</param>
        /// <returns>设置结果</returns>
        public bool SetMinThreads(int minThreadNum)
        {
            return ThreadPool.SetMinThreads(minThreadNum, minThreadNum);
        }
        #endregion

        #region 将操作系统句柄绑定到线程池
        /// <summary>
        /// 将操作系统句柄绑定到线程池
        /// </summary>
        /// <param name="osHandle">系统句柄</param>
        /// <returns>设置结果</returns>
        public bool SetBindHandle(IntPtr osHandle)
        {
            try
            {
                return ThreadPool.BindHandle(osHandle);
            }
            catch (Exception e)
            {
                //句柄无效
                return false;
            }
            
        }
        #endregion        

    }
}
