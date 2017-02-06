using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// 线程池操作接口
    /// </summary>
    interface IThreadPoolOperate
    {
        /// <summary>
        /// 添加执行线程
        /// </summary>
        /// <param name="callback">要执行的方法</param>
        /// <param name="state">方法的参数</param>
        /// <returns>添加结果</returns>
        bool AddThread(WaitCallback callback, object state);

        /// <summary>
        /// 获取线程池剩余线程数
        /// </summary>
        /// <returns>线程池剩余线程数</returns>
        int GetUnusedThread();

        /// <summary>
        /// 获取线程池最大线程数
        /// </summary>
        /// <returns>线程池最大线程数</returns>
        int GetMaxThreads();

        /// <summary>
        /// 获取最小空闲线程数
        /// </summary>
        /// <returns>最小空闲线程数</returns>
        int GetMinThreads();

        /// <summary>
        /// 设置线程池最大线程数
        /// </summary>
        /// <param name="maxThreadNum">最大线程数</param>
        /// <returns>设置结果</returns>
        bool SetMaxThreads(int maxThreadNum);

        /// <summary>
        /// 设置线程池空闲数
        /// </summary>
        /// <param name="minThreadNum">空闲数</param>
        /// <returns>设置结果</returns>
        bool SetMinThreads(int minThreadNum);

        /// <summary>
        /// 将操作系统句柄绑定到线程池
        /// </summary>
        /// <param name="osHandle">系统句柄</param>
        /// <returns>设置结果</returns>
        bool SetBindHandle(IntPtr osHandle);
    }
}
