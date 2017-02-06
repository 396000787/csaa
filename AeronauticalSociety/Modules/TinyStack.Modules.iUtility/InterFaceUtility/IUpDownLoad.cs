using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// 上传下载接口
    /// </summary>
    interface IUpDownLoad
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="address">服务地址</param>
        /// <param name="fileName">文件路径</param>
        /// <returns>上传后的相对路径</returns>
        string UpLoadFile(Uri address, string fileName, bool isShare);

        /// <summary>
        /// 批量上传文件
        /// </summary>
        /// <param name="address">服务路径</param>
        /// <param name="fileList">文件路径列表</param>
        /// <returns>上传后的相对路径列表</returns>
        string[] UpLoadFiles(Uri address, string[] downFileList);

        /// <summary>
        /// 上传文件(异步)
        /// </summary>
        /// <param name="address">服务地址</param>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        void UpLoadFileAsync(Uri address, string fileName);

        /// <summary>
        /// 批量上传文件(异步)
        /// </summary>
        /// <param name="address">服务路径</param>
        /// <param name="fileList">文件路径列表</param>
        /// <returns></returns>
        void UpLoadFilesAsync(Uri address, string[] downFileList);

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="address">下载地址</param>
        /// <param name="fileFull">文件全路径</param>
        /// <returns></returns>
        void DownLoadFile(Uri address, string fileFull);

        /// <summary>
        /// 批量下载文件
        /// </summary>
        /// <param name="downList">下载文件列表(下载地址,文件全路径)</param>
        /// <returns></returns>
        void DownLoadFiles(Dictionary<Uri, string> downList);

        /// <summary>
        /// 下载文件(异步)
        /// </summary>
        /// <param name="address">下载地址</param>
        /// <param name="fileFull">文件全路径</param>
        /// <returns></returns>
        void DownLoadFileAsync(Uri address, string fileFull);

        /// <summary>
        /// 批量下载文件(异步)
        /// </summary>
        /// <param name="downList">下载文件列表(下载地址,文件全路径)</param>
        /// <returns></returns>
        void DownLoadFilesAsync(Dictionary<Uri, string> downList);
    }
}
