using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyStack.Modules.FileUpload.DataModels
{
    public class FileInfoResult
    {
        /// <summary>
        /// 0: 操作成功
        ///-1: 操作失败
        ///-2：IAP接入信息错误
        ///-99：其他错误
        /// </summary>
        public int returnCode { get; set; }
        /// <summary>
        /// 返回相关信息
        /// </summary>
        public string returnMessage { get; set; }
        /// <summary>
        /// 文件详细信息
        /// </summary>
        private List<FileDetail> _FilesInfo = new List<FileDetail>();
        /// <summary>
        /// 文件详细信息
        /// </summary>
        public List<FileDetail> FilesInfo { get { return _FilesInfo; } set { _FilesInfo = value; } }
    }
}
