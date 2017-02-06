using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 栏目分类
    /// </summary>
    public class Arctype
    {
        /// <summary>
        /// 栏目ID
        /// </summary>
        public Int32 id { get; set; }

        /// <summary>
        /// 上级栏目ID
        /// </summary>
        public Int32 reid { get; set; }

        /// <summary>
        /// 顶级栏目ID
        /// </summary>
        public Int32 topid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Int32 sortrank { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string typename { get; set; }
        /// <summary>
        /// 栏目目录
        /// </summary>
        public string typedir { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public string isdefault { get; set; }

        /// <summary>
        /// 默认名称
        /// </summary>
        public string defaultname { get; set; }

        /// <summary>
        /// 是否支持投稿
        /// </summary>
        public Int32 issend { get; set; }

        /// <summary>
        /// 栏目频道类型
        /// </summary>
        public Int32 channeltype { get; set; }

        /// <summary>
        /// 最大页面数
        /// </summary>
        public Int32 maxpage { get; set; }

        /// <summary>
        /// 栏目属性
        /// </summary>
        public Int32 ispart { get; set; }

        /// <summary>
        /// 浏览权限
        /// </summary>
        public Int32 corank { get; set; }

        /// <summary>
        /// 频道页模板
        /// </summary>
        public string tempindex { get; set; }

        /// <summary>
        /// 列表页模板
        /// </summary>
        public string templist { get; set; }

        /// <summary>
        /// 内容页模板
        /// </summary>
        public string temparticle { get; set; }

        /// <summary>
        /// 文章命名规则
        /// </summary>
        public string namerule { get; set; }

        /// <summary>
        /// 列表命名规则
        /// </summary>
        public string namerule2 { get; set; }

        /// <summary>
        /// 模型名称
        /// </summary>
        public string modname { get; set; }

        /// <summary>
        /// 栏目描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 栏目关键词
        /// </summary>
        public string keywords { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string seotitle { get; set; }

        /// <summary>
        /// 多站点支持
        /// </summary>
        public Int32 moresite { get; set; }

        /// <summary>
        /// 站点根目录
        /// </summary>
        public string sitepath { get; set; }

        /// <summary>
        /// 绑定域名
        /// </summary>
        public string siteurl { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public Int32 ishidden { get; set; }

        /// <summary>
        /// 交叉栏目
        /// </summary>
        public Int32 cross { get; set; }

        /// <summary>
        /// 交叉ID
        /// </summary>
        public string crossid { get; set; }

        /// <summary>
        /// 栏目内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 栏目小分类
        /// </summary>
        public string smalltypes { get; set; }
    }
}
