using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 所在地区
    /// </summary>
    public class Area
    {
        #region id
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        #endregion

        #region 名称
        /// <summary>
        /// 名称
        /// </summary>
        public string areaName { get; set; }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        public int index { get; set; }
        #endregion 
    }
}
