using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Log
{
    public class LogFactory
    {
        Type _type = null;
        public LogFactory(Type type)
        {
            _type = type;
        }

      

        #region 输出日志
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="ModuleName">模块名称</param>
        /// <param name="MethodName">方法名称</param>
        /// <param name="Message">日志信息</param>
        public void CreateLog(LogType logType, string ModuleName, string MethodName, string Message)
        {
            WriteLog(logType, ModuleName, MethodName, Message);
        }
        #endregion

        #region 输出日志
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="ModuleName">模块名称</param>
        /// <param name="MethodName">方法名称</param>
        /// <param name="err">日志信息</param>
        public void CreateLog(LogType logType, string ModuleName, string MethodName, Exception err)
        {
            WriteLog(logType, ModuleName, MethodName, err.ToString());
        }
        #endregion

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Message"></param>
        private void WriteLog(LogType type, string ModuleName, string MethodName, string Message)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} {1} 日志类型：{2}", time, _type.FullName, type));
            sb.AppendLine(string.Format("程序集名称：{0}", _type.FullName));
            sb.AppendLine(string.Format("模块名称：{0}", ModuleName));
            sb.AppendLine(string.Format("方法名称：{0}", MethodName));
            sb.AppendLine(string.Format("日志信息：{0}", Message));
            sb.AppendLine("==============================================");
            WriteToFile(type, sb.ToString());
            SaveLog(type, time, _type.FullName, ModuleName, MethodName, Message);
        }

        #region 保存错误信息
        /// <summary>
        /// 保存错误信息
        /// </summary>
        private void SaveLog(LogType type,string time,string FullName ,string ModuleName, string MethodName, string Message) {
            try
            {
                string sql = "insert into  de2_errlog (time,assembly,type,moduleName,methodName,message) VALUES(@time,@assembly,@type,@moduleName,@methodName,@message)";
                MySqlClient _MySqlClient = new MySqlClient();
                _MySqlClient.ExecuteNonQuery(sql,new MySqlParameter("time", time),
                    new MySqlParameter("assembly", FullName),
                    new MySqlParameter("type", type),
                    new MySqlParameter("moduleName", ModuleName),
                    new MySqlParameter("methodName", MethodName),
                    new MySqlParameter("message", Message)
                    );
            }
            catch (Exception)
            {
                
            }
        }
        #endregion


        public static string Locker = "Locker";

        #region 生成日志文件
        /// <summary>
        /// 生成日志文件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Message"></param>
        private void WriteToFile(LogType type, string Message)
        {
            //日志根目录
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            //日志文件名称
            string logFileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            lock (Locker)
            {
                //判断路径是否存在
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                //判断日志类型
                switch (type)
                {
                    case LogType.Debug:
                        logFilePath = Path.Combine(logFilePath, "Debug");
                        break;
                    case LogType.Error:
                        logFilePath = Path.Combine(logFilePath, "Error");
                        break;
                    case LogType.Fatal:
                        logFilePath = Path.Combine(logFilePath, "Fatal");
                        break;
                    case LogType.Info:
                        logFilePath = Path.Combine(logFilePath, "Info");
                        break;
                    case LogType.Trace:
                        logFilePath = Path.Combine(logFilePath, "Trace");
                        break;
                    case LogType.Warn:
                        logFilePath = Path.Combine(logFilePath, "Warn");
                        break;
                    default:
                        logFilePath = Path.Combine(logFilePath, "Error");
                        break;
                }
                //判断路径是否存在
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                string LogFile = Path.Combine(logFilePath, logFileName);
                //输出日志文件
                File.AppendAllText(LogFile, Message);
            }
        }
        #endregion 
    }
}
