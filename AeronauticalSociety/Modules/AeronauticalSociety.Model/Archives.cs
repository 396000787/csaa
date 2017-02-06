using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 文档基础信息表
    /// </summary>
    public class Archives
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        public int id { get; set; }

        #region 栏目ID
        /// <summary>
        /// 栏目ID
        /// </summary>
        public Int32 typeid { get; set; }
        #endregion

        #region 栏目名称
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string typename { get; set; }
        #endregion

        #region 副栏目ID
        /// <summary>
        /// 副栏目ID
        /// </summary>
        public Int32 typeid2 { get; set; }
        #endregion

        /// <summary>
        /// 文档排序
        /// </summary>
        public Int32 sortrank { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string flag { get; set; }

        /// <summary>
        /// 是否生成HTML
        /// </summary>
        public Int32 ismake { get; set; }

        /// <summary>
        /// 频道模型
        /// </summary>
        public Int32 channel { get; set; }

        /// <summary>
        /// 浏览权限
        /// </summary>
        public Int32 arcrank { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public Int32 click { get; set; }

        /// <summary>
        /// 需要消耗金币
        /// </summary>
        public Int32 money { get; set; }

        /// <summary>
        /// 文档标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 短标题
        /// </summary>
        public string shorttitle { get; set; }

        /// <summary>
        /// 标题颜色
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string writer { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string litpic { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public Int32 pubdate { get; set; }

        /// <summary>
        /// 投稿日期
        /// </summary>
        public Int32 senddate { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public Int32 mid { get; set; }

        /// <summary>
        /// 文档关键词
        /// </summary>
        public string keywords { get; set; }

        /// <summary>
        /// 最后回复
        /// </summary>
        public Int32 lastpost { get; set; }

        /// <summary>
        /// 消耗积分
        /// </summary>
        public Int32 scores { get; set; }

        /// <summary>
        /// 好评
        /// </summary>
        public Int32 goodpost { get; set; }

        /// <summary>
        /// 差评
        /// </summary>
        public Int32 badpost { get; set; }

        /// <summary>
        /// 不允许回复
        /// </summary>
        public Int32 notpost { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 自定义文件名
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// 负责审核管理员的ID
        /// </summary>
        public Int32 dutyadmin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 tackid { get; set; }

        /// <summary>
        /// 自定义类别
        /// </summary>
        public Int32 mtype { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public Int32 weight { get; set; }
    }
}
