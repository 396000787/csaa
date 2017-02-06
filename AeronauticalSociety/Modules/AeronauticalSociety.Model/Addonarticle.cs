using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 文章内容
    /// </summary>
    public class Addonarticle
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public Int32 aid { get; set; }

        /// <summary>
        /// 栏目ID
        /// </summary>
        public Int32 typeid { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 跳转URL
        /// </summary>
        public string redirecturl { get; set; }

        /// <summary>
        /// 自定义模板
        /// </summary>
        public string templet { get; set; }

        /// <summary>
        /// 用户IP
        /// </summary>
        public string userip { get; set; }
    }
}
