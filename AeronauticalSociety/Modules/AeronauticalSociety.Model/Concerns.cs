using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 关注作者
    /// </summary>
    public class Concerns
    {
        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        #endregion

        #region 作者编号
        /// <summary>
        /// 作者编号
        /// </summary>
        public int authorID { get; set; }
        #endregion

        #region 作者名称
        /// <summary>
        /// 作者名称
        /// </summary>
        public string authorName { get; set; }
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
