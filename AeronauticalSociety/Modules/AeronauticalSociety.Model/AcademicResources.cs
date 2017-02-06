using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 学术资源字表
    /// </summary>
    public class AcademicResources
    {
        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 id { get; set; }
        #endregion

        #region 名称
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        #endregion

        #region 父级ID
        /// <summary>
        /// 父级ID
        /// </summary>
        public Int32 parentID { get; set; }
        #endregion

        #region 学术资源类型ID
        /// <summary>
        /// 学术资源类型ID
        /// </summary>
        public Int32 sourceTypeID { get; set; }
        #endregion

        #region 学术资源类型名称
        /// <summary>
        /// 学术资源类型名称
        /// </summary>
        public string sourceTypeName { get; set; }
        #endregion

        #region 创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        #endregion

        #region 创建人ID
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Int32 createUserID { get; set; }
        #endregion

        #region 创建名称
        /// <summary>
        /// 创建名称
        /// </summary>
        public string createUserName { get; set; }
        #endregion

        #region 是否有目录
        /// <summary>
        /// 是否有目录
        /// </summary>
        public bool isMenu { get; set; }
        #endregion

        #region 是否有子列表
        /// <summary>
        /// 是否有子列表
        /// </summary>
        public bool isChild { get; set; }
        #endregion

        #region 作者
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        #endregion

        #region 年度
        /// <summary>
        /// 年度
        /// </summary>
        public string year { get; set; }
        #endregion

        #region 期数
        /// <summary>
        /// 期数
        /// </summary>
        public string periodNumber { get; set; }
        #endregion

        #region 是否对外发布
        /// <summary>
        /// 是否对外发布
        /// </summary>
        public bool isPublish { get; set; }
        #endregion

        #region 文件地址
        /// <summary>
        /// 文件地址
        /// </summary>
        public string resourceFile { get; set; }
        #endregion

        #region 最后修改时间
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime lastModifyTime { get; set; }
        #endregion

        #region 最后修改人ID
        /// <summary>
        /// 最后修改人ID
        /// </summary>
        public int lastModifyUsrID { get; set; }
        #endregion

        #region 最后修改人名称
        /// <summary>
        /// 最后修改人名称
        /// </summary>
        public string lastModifyUserName { get; set; }
        #endregion
    }
}
