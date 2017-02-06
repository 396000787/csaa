using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyStack.Modules.iUtility
{
    // 这个类是为了当前代码中分页写法比较混乱而临时创建的，可能会被删除或重构。
    /// <summary>
    /// 分页帮助类。
    /// </summary>
    public static class PagingHelper
    {
        /// <summary>
        /// 从指定的集合中查询出一页。
        /// </summary>
        /// <typeparam name="T">集合元素类型。</typeparam>
        /// <param name="source">目标集合。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="pageIndex">页索引。从0开始计算。</param>
        /// <param name="pageCount">外部参数。总页数。</param>
        /// <returns>分页后的集合对象。</returns>
        public static IEnumerable<T> TakePage<T>(this IEnumerable<T> source, int pageSize, int pageIndex, out int pageCount)
        {
            int totalCount = source.Count();
            pageCount = pageSize == 0 ? 1 : (totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1);
            return pageSize > 0 ? source.Skip(pageSize * pageIndex).Take(pageSize) : source;
        }

        /// <summary>
        /// 从指定的集合中查询出一页。
        /// </summary>
        /// <typeparam name="T">集合元素类型。</typeparam>
        /// <param name="source">目标集合。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="pageIndex">页索引。从0开始计算。</param>
        /// <returns>分页后的集合对象。</returns>
        public static IEnumerable<T> TakePage<T>(this IEnumerable<T> source, int pageSize, int pageIndex)
        {
            int pageCount;
            return source.TakePage<T>(pageSize, pageIndex, out pageCount);
        }
    }
}
