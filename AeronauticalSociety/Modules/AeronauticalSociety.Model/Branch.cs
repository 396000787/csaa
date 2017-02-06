using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 专业分会
    /// </summary>
    public class Branch
    {
        #region id
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        #endregion

        #region 专业分会名称 
        /// <summary>
        /// 专业分会名称
        /// </summary>
        public string branchName { get; set; }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        public int index { get; set; }
        #endregion
    }
}
