using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 收藏文章
    /// </summary>
    public class Collection
    {
        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        #endregion

        #region 文章编号
        /// <summary>
        /// 文章编号    
        /// </summary>
        public int aid { get; set; }
        #endregion

        #region 创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        #endregion

        #region 收藏人ID
        /// <summary>
        /// 收藏人ID
        /// </summary>
        public int createID { get; set; }
        #endregion

    }
}
