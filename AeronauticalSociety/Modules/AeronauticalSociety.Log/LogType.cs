using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Log
{
    public enum LogType
    {
        /// <summary>
        /// 跟踪。
        /// </summary>
        Trace = 1,
        /// <summary>
        /// 调试。
        /// </summary>
        Debug = 2,
        /// <summary>
        /// 信息。
        /// </summary>
        Info = 3,
        /// <summary>
        /// 警告。
        /// </summary>
        Warn = 4,
        /// <summary>
        /// 错误。
        /// </summary>
        Error = 5,
        /// <summary>
        /// 严重错误。
        /// </summary>
        Fatal = 6,      
    }
}
