using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 工作计划类型
    /// </summary>
    public class WorkPlanType
    {
        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        #endregion

        #region 名称
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        #endregion
    }
}
