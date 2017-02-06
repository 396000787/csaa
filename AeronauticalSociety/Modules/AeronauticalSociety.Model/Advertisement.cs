using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 广告
    /// </summary>
    public class Advertisement
    {
        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        #endregion

        #region 标题
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        #endregion

        #region 目标路径
        /// <summary>
        /// 目标路径
        /// </summary>
        public string targetUrl { get; set; }
        #endregion

        #region 图片路径
        /// <summary>
        /// 图片路径
        /// </summary>
        public string imageUrl { get; set; }
        #endregion

        #region 图片Base64编码
        /// <summary>
        /// 图片Base64编码
        /// </summary>
        public string imageBase64 { get; set; }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        public int index { get; set; }
        #endregion

        #region 是否有效
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool isVail { get; set; }
        #endregion

        #region 点击次数
        /// <summary>
        /// 点击次数
        /// </summary>
        private int _count = 0;
        /// <summary>
        /// 点击次数
        /// </summary>
        public int count { get { return _count; } set { _count = value; } }
        #endregion

        #region 最后修改时间
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime lastModifyTime { get; set; }
        #endregion

        #region 最后修改时间
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime regTime { get; set; }
        #endregion
    }
}
