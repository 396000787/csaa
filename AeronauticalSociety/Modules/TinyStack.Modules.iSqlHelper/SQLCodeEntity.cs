using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyStack.Modules.iSqlHelper
{
    /// <summary>
    /// SQL语句存储实体类
    /// </summary>
    public class SQLCodeEntity
    {
        #region SQL序号
        /// <summary>
        /// SQL序号
        /// </summary>
        private String _SqlNum;
        /// <summary>
        /// SQL序号
        /// </summary>
        public String SqlNum
        {
            get { return _SqlNum; }
            set { _SqlNum = value; }
        }
        #endregion

        #region SQL说明
        /// <summary>
        /// SQL说明
        /// </summary>
        private String _SqlDescription;
        /// <summary>
        /// SQL说明
        /// </summary>
        public String SqlDescription
        {
            get { return _SqlDescription; }
            set { _SqlDescription = value; }
        }
        #endregion
        
        #region SQL语句
        /// <summary>
        /// SQL语句
        /// </summary>
        private String _SQLString;
        /// <summary>
        /// SQL语句
        /// </summary>
        public String SQLString
        {
            get { return _SQLString; }
            set { _SQLString = value; }
        }
        #endregion
    }
}
