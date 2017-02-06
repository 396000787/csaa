using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TinyStack.Modules.FileUpload.DataModels
{
    public class FileDetail
    {
        /// <summary>
        /// 32位唯一文件ID
        /// </summary>
        public string FileID { get; set; }
        /// <summary>
        /// 上传文件的应用系统ID
        /// </summary>
        public string SystemID { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }
        /// <summary>
        /// 文件上传时间
        /// </summary>
        public string UploadDate { get; set; }
        /// <summary>
        /// 删除日期
        /// </summary>
        public string DeleteDate { get; set; }
        /// <summary>
        /// 上传人的UserID
        /// </summary>
        public string UploadBy { get; set; }
        /// <summary>
        /// 删除人的UserID
        /// </summary>
        public string DeleteBy { get;set;}
        /// <summary>
        /// 文件描述
        /// </summary>
        public string FileDesc { get;set;}
        /// <summary>
        /// 文件下载地址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        ///删除标识
        /// </summary>
        [Index]
        public bool IIsDelete { get; set; }

    }
}
