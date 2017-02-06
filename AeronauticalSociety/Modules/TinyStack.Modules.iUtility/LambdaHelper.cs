using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace TinyStack.Modules.iUtility
{
    public static class LambdaHelper
    {
        /// <summary>
        /// 生成模糊搜索的拉姆达表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">关键字</param>
        /// <param name="propertys">搜索字段</param>
        /// <returns></returns>
        public static IEnumerable<T> FuzzySearch<T>(this IEnumerable<T> source, string keys, string[] propertys)
        {
            keys = keys.Trim();
            if (string.IsNullOrEmpty(keys)) return null;

            var query = source.AsQueryable();
            Expression<Func<T, bool>> lambda = null;

            ParameterExpression paramExpr = Expression.Parameter(typeof(T), "d");

            List<Expression> exprList = new List<Expression>();

            foreach (string property in propertys)
            {
                exprList.Add(GetContainExpression(paramExpr, property, keys));
            }

            Expression whereExpr = null;
            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.Or(whereExpr, expr);
            }

            if (whereExpr != null)
                lambda = Expression.Lambda<Func<T, bool>>(whereExpr, paramExpr);

            if (lambda == null) return null;

            query = query.Where(lambda);

            return query.AsEnumerable();
        }

        #region private method

        /// <summary>
        /// 获取lambda表达式，value用空格隔开组成或的关系,属性（字段）与value的关系是Contain（包含）
        /// </summary>
        /// <param name="paramExpr"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Expression GetContainExpression(ParameterExpression paramExpr, string property, string value)
        {
            Expression whereExpr = null;

            List<Expression> exprList = new List<Expression>();
            MethodInfo containsMethod = typeof(string).GetMethod("Contains");

            foreach (string s in value.Split(' '))
            {
                MemberExpression propExpr = Expression.PropertyOrField(paramExpr, property);
                ConstantExpression valueExpr = Expression.Constant(s, typeof(string));
                MethodCallExpression containsExpr = Expression.Call(propExpr, containsMethod, valueExpr);
                exprList.Add(containsExpr);
            }

            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.Or(whereExpr, expr);
            }

            return whereExpr;
        }

        /// <summary>
        /// 获取lambda表达式，value用空格隔开组成或的关系,属性（字段）与value的关系是Equal（相等）
        /// </summary>
        /// <param name="paramExpr"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Expression GetEqualExpression(ParameterExpression paramExpr, string property, string value)
        {
            Expression whereExpr = null;

            List<Expression> exprList = new List<Expression>();

            foreach (string s in value.Split(' '))
            {
                MemberExpression propExpr = Expression.PropertyOrField(paramExpr, property);
                ConstantExpression valueExpr = Expression.Constant(s, typeof(string));
                BinaryExpression expr = Expression.Equal(propExpr, valueExpr);
                exprList.Add(expr);
            }

            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.Or(whereExpr, expr);
            }

            return whereExpr;
        }

        /// <summary>
        /// 获取lambda表达式，value用空格隔开组成或的关系,属性（字段）与value的关系是Contain（包含）
        /// </summary>
        /// <param name="paramExpr"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Expression GetContainExpressionAnd(ParameterExpression paramExpr, string property, string value)
        {
            Expression whereExpr = null;

            List<Expression> exprList = new List<Expression>();
            MethodInfo containsMethod = typeof(string).GetMethod("Contains");

            foreach (string s in value.Split(' '))
            {
                MemberExpression propExpr = Expression.PropertyOrField(paramExpr, property);
                ConstantExpression valueExpr = Expression.Constant(s, typeof(string));
                MethodCallExpression containsExpr = Expression.Call(propExpr, containsMethod, valueExpr);
                exprList.Add(containsExpr);
            }

            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.And(whereExpr, expr);
            }

            return whereExpr;
        }

        /// <summary>
        /// 获取lambda表达式，value用空格隔开组成或的关系,属性（字段）与value的关系是Equal（相等）
        /// </summary>
        /// <param name="paramExpr"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Expression GetEqualExpressionAnd(ParameterExpression paramExpr, string property, string value)
        {
            Expression whereExpr = null;

            List<Expression> exprList = new List<Expression>();

            foreach (string s in value.Split(' '))
            {
                MemberExpression propExpr = Expression.PropertyOrField(paramExpr, property);
                ConstantExpression valueExpr = Expression.Constant(s, typeof(string));
                BinaryExpression expr = Expression.Equal(propExpr, valueExpr);
                exprList.Add(expr);
            }

            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.And(whereExpr, expr);
            }

            return whereExpr;
        }
        #endregion
    }
}
