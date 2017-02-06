using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 职务
    /// </summary>
    public class Post
    {
        #region 编号
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
        #endregion

        #region 职务名称
        /// <summary>
        /// 职务名称
        /// </summary>
        public string postName { get; set; }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        public int index { get; set; }
        #endregion
    }
}
