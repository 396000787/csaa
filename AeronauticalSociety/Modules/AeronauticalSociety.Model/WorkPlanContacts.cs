using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 工作计划联系人
    /// </summary>
    public class WorkPlanContacts
    {
        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        #endregion

        #region 工作计划ID
        /// <summary>
        /// 工作计划ID
        /// </summary>
        public int wid { get; set; }
        #endregion

        #region 联系人ID
        /// <summary>
        /// 联系人ID
        /// </summary>
        public int contactsID { get; set; }
        #endregion

        #region 联系人姓名
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string contactsName { get; set; }
        #endregion

        #region 联系电话
        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }
        #endregion

        #region  状态
        /// <summary>
        /// 状态
        /// </summary>
        public string statue { get; set; }
        #endregion

    }
}
