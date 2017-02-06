using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 图集信息表
    /// </summary>
    public class Addonimages
    {
        /// <summary>
        /// 图集ID(文章ID)
        /// </summary>
        public Int32 aid { get; set; }

        /// <summary>
        /// 栏目ID
        /// </summary>
        public Int32 typeid { get; set; }

        /// <summary>
        /// 显示列表样式
        /// </summary>
        public Int32 pagestyle { get; set; }

        /// <summary>
        /// 最大宽度
        /// </summary>
        public Int32 maxwidth { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string imgurls { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public Int32 row { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        public Int32 col { get; set; }

        /// <summary>
        /// 特殊选项（下载远程图片、从ZIP压缩包中解压图片、网上复制图片)
        /// </summary>
        public Int32 isrm { get; set; }

        /// <summary>
        /// 缩略图最大宽度
        /// </summary>
        public Int32 ddmaxwidth { get; set; }

        /// <summary>
        /// 每页显示图片数量
        /// </summary>
        public Int32 pagepicnum { get; set; }

        /// <summary>
        /// 自定义模板
        /// </summary>
        public string templet { get; set; }

        /// <summary>
        /// 用户IP
        /// </summary>
        public string userip { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string redirecturl { get; set; }

        /// <summary>
        /// 图集详细说明
        /// </summary>
        public string body { get; set; }
    }
}
