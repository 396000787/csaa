using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    public class ArticleDetial
    {
        /// <summary>
        /// 文档基础信息
        /// </summary>
        public Archives BaseInfro { get; set; }
        /// <summary>
        /// 文档内容信息
        /// </summary>
        public Addonarticle Body { get; set; }
        /// <summary>
        /// 是否被关注
        /// </summary>
        public bool isFouce { get; set; }
        /// <summary>
        /// 是否被收藏
        /// </summary>
        public bool isCollection { get; set; }
    }
}
