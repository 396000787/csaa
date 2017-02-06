using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.BusinessLayer.Controllers
{
    public class log
    {
        private static string LockKey = "LockKey";
        public static void SaveLog(String TaskMess)
        {
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\");

                }

                DateTime now = DateTime.Now;
                String LogFile = AppDomain.CurrentDomain.BaseDirectory + "\\logs\\" + now.ToString("yyyy-MM-dd") + ".log";
                lock (LockKey)
                {
                    using (FileStream fs = new FileStream(LogFile, FileMode.Append, FileAccess.Write))
                    {
                        byte[] datetimefile = System.Text.Encoding.Default.GetBytes(now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n");
                        fs.Write(datetimefile, 0, datetimefile.Length);
                        if (!String.IsNullOrEmpty(TaskMess))
                        {
                            byte[] data = System.Text.Encoding.Default.GetBytes(TaskMess + "\r\n");
                            fs.Write(data, 0, data.Length);
                        }
                        fs.Flush();
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
