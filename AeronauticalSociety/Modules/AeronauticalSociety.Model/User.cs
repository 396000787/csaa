using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class User
    {
        #region 用户编号主键
        /// <summary>
        /// 用户编号
        /// </summary>
        public Int32 id { get; set; }
        #endregion

        #region 用户Key
        /// <summary>
        /// 用户Key
        /// </summary>
        public Guid userKey { get; set; }
        #endregion

        #region 用户登录名称
        /// <summary>
        /// 用户登录名称
        /// </summary>
        public string loginName { get; set; }
        #endregion

        #region 用户登录密码
        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string password { get; set; }
        #endregion

        #region 用户名称
        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName { get; set; }
        #endregion

        #region 会员账号
        /// <summary>
        /// 会员账号
        /// </summary>
        public string memberCode { get; set; }
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

        #region 性别
        /// <summary>
        /// 性别
        /// </summary>
        public int sex { get; set; }
        #endregion

        #region  注册时间
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime regTime { get; set; }
        #endregion

        #region 最后编辑时间
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime lastModifyTime { get; set; }
        #endregion

        #region 管理员类型
        /// <summary>
        /// 管理员类型
        /// </summary>
        public int adminType { get; set; }
        #endregion



    }
}
