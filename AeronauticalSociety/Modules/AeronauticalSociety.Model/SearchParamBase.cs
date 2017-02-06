using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Model
{
    public class SearchParamBase
    {
        /// <summary>
        /// 起始行
        /// </summary>
        public int StartRow { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int PageSize { get; set; }
    }
}
