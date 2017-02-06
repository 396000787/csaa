using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    public class MenuModel
    {
        #region 编号
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        public int Index { get; set; }
        #endregion

        #region 名称
        /// <summary>
        /// 名称
        /// </summary>
        public string MenuName { get; set; }
        #endregion

        #region 图标
        /// <summary>
        /// 图标
        /// </summary>
        public string Ico { get; set; }
        #endregion

        #region 跳转Url
        /// <summary>
        /// 跳转Url
        /// </summary>
        public string Url { get; set; }
        #endregion

        #region dede栏目分类的ID
        /// <summary>
        /// dede栏目分类的ID
        /// </summary>
        public string TypeID { get; set; }
        #endregion

        #region 是否展示
        /// <summary>
        /// 是否展示
        /// </summary>
        public bool IsShow { get; set; }
        #endregion

    }
}
