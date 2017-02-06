using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 工作计划
    /// </summary>
    public class WorkPlan
    {
        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        #endregion

        #region 年度
        /// <summary>
        /// 年度
        /// </summary>
        public string year { get; set; }
        #endregion

        #region 开始时间
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime { get; set; }
        #endregion

        #region 结束时间
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime { get; set; }
        #endregion

        #region 活动名称
        /// <summary>
        /// 活动名称
        /// </summary>
        public string name { get; set; }
        #endregion

        #region 内容
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        #endregion

        #region 规模
        /// <summary>
        /// 规模
        /// </summary>
        public int scale { get; set; }
        #endregion

        #region 地点-省ID
        /// <summary>
        /// 地点-省ID
        /// </summary>
        public int addressProvinceID { get; set; }
        #endregion

        #region 地点-省
        /// <summary>
        /// 地点-省
        /// </summary>
        public string addressProvince { get; set; }
        #endregion

        #region 地点-市ID
        /// <summary>
        /// 地点-市ID
        /// </summary>
        public int addressCityID { get; set; }
        #endregion

        #region 地点-市
        /// <summary>
        /// 地点-市
        /// </summary>
        public string addressCity { get; set; }
        #endregion

        #region 地点
        /// <summary>
        /// 地点
        /// </summary>
        public string address { get; set; }
        #endregion

        #region 创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        #endregion

        #region 最后修改时间
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime lastModifyTime { get; set; }
        #endregion

        #region 创建人ID
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int createUserID { get; set; }
        #endregion

        #region 工作计划类型ID
        /// <summary>
        /// 工作计划类型ID
        /// </summary>
        public int workPlanTypeID { get; set; }
        #endregion

        #region 工作计划类型名称
        /// <summary>
        /// 工作计划类型名称
        /// </summary>
        public int workPlanTypeName { get; set; }
        #endregion

        #region 联系人
        /// <summary>
        /// 联系人
        /// </summary>
        private List<WorkPlanContacts> _Contacts = new List<WorkPlanContacts>();
        /// <summary>
        /// 联系人
        /// </summary>
        public List<WorkPlanContacts> Contacts { get { return _Contacts; } set { _Contacts = value; } }
        #endregion
    }
}
