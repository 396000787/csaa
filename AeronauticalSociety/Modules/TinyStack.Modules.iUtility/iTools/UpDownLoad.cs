using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TinyStack.Modules.iUtility.InterFaceUtility;
using System.Net;

namespace TinyStack.Modules.iUtility.iTools
{
    /// <summary>
    /// 文件上传下载类
    /// </summary>
    public class UpDownLoad : IUpDownLoad
    {
        #region 定义事件
        //返回数据委托
        public delegate void RetrunData(object sender, Dictionary<string, string> dic);

        #region 上传结束事件
        /// <summary>
        /// 上传结束事件
        /// </summary>
        private event EventHandler _UploadFileCompleted;
        /// <summary>
        /// 上传结束事件
        /// </summary>
        public event EventHandler UploadFileCompleted
        {
            add { _UploadFileCompleted += value; }
            remove { _UploadFileCompleted -= value; }
        }
        #endregion

        #region 上传数据改变事件
        /// <summary>
        /// 上传数据改变事件
        /// </summary>
        private event RetrunData _UploadProgressChanged;
        /// <summary>
        /// 上传数据改变事件
        /// </summary>
        public event RetrunData UploadProgressChanged
        {
            add { _UploadProgressChanged += value; }
            remove { _UploadProgressChanged -= value; }
        }
        #endregion

        #region 下载结束事件
        /// <summary>
        /// 下载结束事件
        /// </summary>
        private event EventHandler _DownloadFileCompleted;
        /// <summary>
        /// 下载结束事件
        /// </summary>
        public event EventHandler DownloadFileCompleted
        {
            add { _DownloadFileCompleted += value; }
            remove { _DownloadFileCompleted -= value; }
        }
        #endregion

        #region 下载数据改变事件
        /// <summary>
        /// 下载数据改变事件
        /// </summary>
        private event RetrunData _DownloadProgressChanged;
        /// <summary>
        /// 下载数据改变事件
        /// </summary>
        public event RetrunData DownloadProgressChanged
        {
            add { _DownloadProgressChanged += value; }
            remove { _DownloadProgressChanged -= value; }
        }
        #endregion

        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="address">服务地址</param>
        /// <param name="fileName">文件路径</param>
        /// <returns>上传后的相对路径</returns>
        public string UpLoadFile(Uri address, string fileName, bool isShare)
        {
            try
            {
                if (isShare)
                {
                    File.Copy(fileName, address.LocalPath, true);
                    return "";
                }
                WebClient client = new WebClient();
                //client.Credentials = new NetworkCredential("test", "test");
                byte[] returnby = client.UploadFile(address, fileName);
                //将返回转换成string
                string retrunStr = System.Text.Encoding.UTF8.GetString(returnby);
                //抛出上传完成事件
                if (_UploadFileCompleted != null)
                {
                    _UploadFileCompleted(this, null);
                }
                return retrunStr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 批量上传文件
        /// <summary>
        /// 批量上传文件
        /// </summary>
        /// <param name="address">服务路径</param>
        /// <param name="fileList">文件路径列表</param>
        /// <returns>上传后的相对路径列表</returns>
        public string[] UpLoadFiles(Uri address, string[] upFileList)
        {
            try
            {
                WebClient client = new WebClient();
                string[] returnStr = new string[upFileList.Count()];
                int count = 0;
                foreach (var item in upFileList)
                {
                    //判断上传文件是否存在
                    if (!File.Exists(item))
                    {
                        continue;
                    }
                    //获取上传文件的物理路径
                    byte[] rb = client.UploadFile(address, item);
                    //将返回字节转换成字符串添加到数组
                    returnStr[count] = System.Text.Encoding.UTF8.GetString(rb);
                    count++;
                }
                //抛出上传完成事件
                if (_UploadFileCompleted != null)
                {
                    _UploadFileCompleted(this, null);
                }
                return returnStr;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 上传文件(异步)
        /// <summary>
        /// 上传文件(异步)
        /// </summary>
        /// <param name="address">服务地址</param>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public void UpLoadFileAsync(Uri address, string fileName)
        {
            try
            {
                //判断要上传的文件是否存在
                if (!File.Exists(fileName))
                {
                    return;
                }
                WebClient client = new WebClient();
                //注册上传完成事件
                client.UploadFileCompleted += (s, e) =>
                {
                    byte[] returnby = e.Result;
                    //将返回转换成string
                    string retrunStr = System.Text.Encoding.UTF8.GetString(returnby);
                    //抛出上传完成事件
                    if (_UploadFileCompleted != null)
                    {
                        _UploadFileCompleted(retrunStr, e);
                    }
                };
                //注册异步上传数据改变事件
                client.UploadProgressChanged += (s, e) =>
                {
                    //抛出上传数据改变事件
                    if (_UploadProgressChanged != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("Progress", e.ProgressPercentage + "%");
                        _UploadProgressChanged(s, dic);
                    }
                };
                client.UploadFileAsync(address, fileName);
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region 批量上传文件(异步)
        /// <summary>
        /// 批量上传文件(异步)
        /// </summary>
        /// <param name="address">服务路径</param>
        /// <param name="fileList">文件路径列表</param>
        /// <returns></returns>
        public void UpLoadFilesAsync(Uri address, string[] upFileList)
        {
            try
            {
                foreach (var item in upFileList)
                {
                    if (!File.Exists(item))
                    {
                        continue;
                    }
                    WebClient client = new WebClient();
                    //注册上传完成事件
                    client.UploadFileCompleted += (s, e) =>
                    {
                        byte[] returnby = e.Result;
                        //将返回转换成string
                        string retrunStr = System.Text.Encoding.UTF8.GetString(returnby);
                        //抛出上传完成事件
                        if (_UploadFileCompleted != null)
                        {
                            _UploadFileCompleted(retrunStr, e);
                        }
                    };
                    //注册异步上传数据改变事件
                    client.UploadProgressChanged += (s, e) =>
                    {
                        //抛出异步上传数据改变事件
                        if (_UploadProgressChanged != null)
                        {
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("Progress", e.ProgressPercentage + "%");
                            _UploadProgressChanged(s, dic);
                        }
                    };
                    client.UploadFileAsync(address, item);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="address">下载地址</param>
        /// <param name="fileFull">文件全路径</param>
        /// <returns></returns>
        public void DownLoadFile(Uri address, string fileFull)
        {
            try
            {
                string filePath = fileFull.Substring(0, fileFull.LastIndexOf('\\'));
                //判断要保存的路径是否存在
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                WebClient client = new WebClient();
                client.DownloadFile(address, fileFull);
                //抛出下载完成事件
                if (_DownloadFileCompleted != null)
                {
                    _DownloadFileCompleted(this, null);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region 批量下载文件
        /// <summary>
        /// 批量下载文件
        /// </summary>
        /// <param name="downList">下载文件列表(下载地址,文件全路径)</param>
        /// <returns></returns>
        public void DownLoadFiles(Dictionary<Uri, string> downList)
        {
            try
            {
                WebClient client = new WebClient();
                foreach (var item in downList)
                {
                    string filePath = item.Value.Substring(0, item.Value.LastIndexOf('\\'));
                    //判断要保存的路径是否存在
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    client.DownloadFile(item.Key, item.Value);
                }
                //抛出下载完成事件
                if (_DownloadFileCompleted != null)
                {
                    _DownloadFileCompleted(this, null);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region 下载文件(异步)
        /// <summary>
        /// 下载文件(异步)
        /// </summary>
        /// <param name="address">下载地址</param>
        /// <param name="fileFull">文件全路径</param>
        /// <returns></returns>
        public void DownLoadFileAsync(Uri address, string fileFull)
        {
            try
            {
                string filePath = fileFull.Substring(0, fileFull.LastIndexOf('\\'));
                //判断要保存的路径是否存在
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                WebClient client = new WebClient();
                //注册下载完成事件
                client.DownloadFileCompleted += (s, e) =>
                {
                    //抛出下载完成事件
                    if (_DownloadFileCompleted != null)
                    {
                        _DownloadFileCompleted(s, e);
                    }
                };
                long old = 0;
                long speed = 0;
                //注册异步下载数据改变事件
                client.DownloadProgressChanged += (s, e) =>
                {
                    //计算速度
                    speed = (e.BytesReceived - old) / 1024;
                    //计算剩余时间
                    string lastTime = (e.TotalBytesToReceive - e.BytesReceived) == 0 ? "已完成" : "未知";
                    if (speed != 0)
                    {
                        int last = Convert.ToInt32(((e.TotalBytesToReceive - e.BytesReceived) / 1024) / speed);
                        TimeSpan ts = new TimeSpan(0, 0, last);
                        lastTime = (int)ts.TotalHours + "小时" + ts.Minutes + "分钟" + ts.Seconds + "秒";
                    }
                    //记录上次文件大小
                    old = e.BytesReceived;

                    //抛出下载数据改变事件
                    if (_DownloadProgressChanged != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("Speed", speed.ToString() + "KB/S");
                        dic.Add("LastTime", lastTime);
                        dic.Add("FullSize", e.TotalBytesToReceive / 1024 + "KB");
                        dic.Add("LastSize", e.BytesReceived / 1024 + "KB");
                        dic.Add("Progress", e.ProgressPercentage + "%");
                        _DownloadProgressChanged(s, dic);
                    }
                };
                client.DownloadFileAsync(address, fileFull);
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region 批量下载文件(异步)
        /// <summary>
        /// 批量下载文件(异步)
        /// </summary>
        /// <param name="downList">下载文件列表(下载地址,文件全路径)</param>
        /// <returns></returns>
        public void DownLoadFilesAsync(Dictionary<Uri, string> downList)
        {
            try
            {
                foreach (var item in downList)
                {
                    string filePath = item.Value.Substring(0, item.Value.LastIndexOf('\\'));
                    //判断要保存的路径是否存在
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    WebClient client = new WebClient();

                    //注册下载完成事件
                    client.DownloadFileCompleted += (s, e) =>
                    {
                        //抛出下载完成事件
                        if (_DownloadFileCompleted != null)
                        {
                            _DownloadFileCompleted(s, e);
                        }
                    };

                    long old = 0;
                    long speed = 0;
                    //注册异步下载数据改变事件
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        //计算速度
                        speed = (e.BytesReceived - old) / 1024;
                        //计算剩余时间
                        string lastTime = (e.TotalBytesToReceive - e.BytesReceived) == 0 ? "已完成" : "未知";
                        if (speed != 0)
                        {
                            int last = Convert.ToInt32(((e.TotalBytesToReceive - e.BytesReceived) / 1024) / speed);
                            TimeSpan ts = new TimeSpan(0, 0, last);
                            lastTime = (int)ts.TotalHours + "小时" + ts.Minutes + "分钟" + ts.Seconds + "秒";
                        }

                        //记录上次文件大小
                        old = e.BytesReceived;

                        //抛出下载数据改变事件
                        if (_DownloadProgressChanged != null)
                        {
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("Speed", speed.ToString() + "KB/S");
                            dic.Add("LastTime", lastTime);
                            dic.Add("FullSize", e.TotalBytesToReceive / 1024 + "KB");
                            dic.Add("LastSize", e.BytesReceived / 1024 + "KB");
                            dic.Add("Progress", e.ProgressPercentage + "%");
                            _DownloadProgressChanged(s, dic);
                        }
                    };
                    client.DownloadFileAsync(item.Key, item.Value);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion
    }
}
