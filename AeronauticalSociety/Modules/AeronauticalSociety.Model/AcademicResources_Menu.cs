using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    /// <summary>
    /// 学术资源文章目录
    /// </summary>
    public class AcademicResources_Menu
    {
        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 id { get; set; }
        #endregion

        #region 标题
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        #endregion

        #region 父级ID
        /// <summary>
        /// 父级ID
        /// </summary>
        public Int32 parentID { get; set; }
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

        #region 创建人名称
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string createUserName { get; set; }
        #endregion

        #region 类型id
        /// <summary>
        /// 类型id
        /// </summary>
        public Int16 sourceTypeID { get; set; }
        #endregion

        #region 类型名称
        /// <summary>
        /// 类型名称
        /// </summary>
        public string sourceTypeName { get; set; }
        #endregion

        #region 作者
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
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

        #region 最后修改时间
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime lastModifyTime { get; set; }
        #endregion

        #region 资源文件路径
        /// <summary>
        /// 资源文件路径
        /// </summary>
        public string resourceFile { get; set; }
        #endregion
    }
}
