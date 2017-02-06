using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    ///会员信息表
    /// </summary>
    public class Member
    {
        #region 主键
        public int id { get; set; }
        #endregion

        #region 会员编号
        /// <summary>
        /// 会员编号
        /// </summary>
        public string memberID { get; set; }
        #endregion

        #region 会员名称
        /// <summary>
        /// 会员名称
        /// </summary>
        public string memberName { get; set; }
        #endregion

        #region 会员等级
        /// <summary>
        /// 会员等级
        /// </summary>
        public int level { get; set; }
        #endregion

        #region 会员等级名称
        /// <summary>
        /// 会员等级名称
        /// </summary>
        public string levelName { get; set; }
        #endregion

        #region 本会任职ID
        /// <summary>
        ///  本会任职ID
        /// </summary>
        public int postID { get; set; }
        #endregion

        #region 本会任职名称
        /// <summary>
        /// 本会任职名称
        /// </summary>
        public string post { get; set; }
        #endregion

        #region 所在地区ID
        /// <summary>
        /// 所在地区ID
        /// </summary>
        public int areaID { get; set; }
        #endregion

        #region 所在地区名称
        /// <summary>
        /// 所在地区名称
        /// </summary>
        public string areaName { get; set; }
        #endregion

        #region 专业分会ID
        /// <summary>
        /// 专业分会ID
        /// </summary>
        public int branchID { get; set; }
        #endregion

        #region 专业分会名称
        /// <summary>
        /// 专业分会名称
        /// </summary>
        public string branchName { get; set; }
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

        #region 通讯地址
        /// <summary>
        /// 通讯地址
        /// </summary>
        public string address { get; set; }
        #endregion

        #region 电子邮件
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string email { get; set; }
        #endregion

        #region 电话
        /// <summary>
        /// 电话
        /// </summary>
        public string mobileNO { get; set; }
        #endregion

        #region 会员状态编码
        /// <summary>
        /// 状态编码
        /// </summary>
        public int stateID { get; set; }
        #endregion

        #region 会员状态
        /// <summary>
        /// 会员状态
        /// </summary>
        public string state { get; set; }
        #endregion

        #region 性别
        /// <summary>
        /// 性别
        /// </summary>
        public Int32 sex { get; set; }
        #endregion

        #region 注册用户ID
        /// <summary>
        /// 注册用户ID
        /// </summary>
        public int userID { get; set; }
        #endregion

        #region 专业id
        /// <summary>
        /// 专业id
        /// </summary>
        public int professionID { get; set; }
        #endregion

        #region 专业名称
        /// <summary>
        /// 专业名称
        /// </summary>
        public string professionName { get; set; }
        #endregion

        #region 年龄
        /// <summary>
        /// 年龄
        /// </summary>
        public int age { get; set; }
        #endregion

        #region 职业ID
        /// <summary>
        /// 职业ID
        /// </summary>
        public int occupationID { get; set; }
        #endregion

        #region 职业名称
        /// <summary>
        /// 职业名称
        /// </summary>
        public string occupationName { get; set; }
        #endregion

        #region 用户级别id
        /// <summary>
        /// 用户级别id
        /// </summary>
        public int userLeveID { get; set; }
        #endregion

        #region 用户级别名称
        /// <summary>
        /// 用户级别名称
        /// </summary>
        public string userLeveName { get; set; }
        #endregion

        #region 登陆名
        /// <summary>
        /// 登陆名
        /// </summary>
        public string loginName { get; set; }
        #endregion
    }
}
