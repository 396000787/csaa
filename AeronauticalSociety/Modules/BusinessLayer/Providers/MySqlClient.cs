using AeronauticalSociety.BusinessLayer.SystemConstant;
using AeronauticalSociety.Log;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using TinyStack.Modules.iSqlHelper;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    public class MySqlClient
    {

        #region 执行Sql语句
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdText, params MySqlParameter[] cmdParms)
        {
            MySQLHelper _MySQLHelper = new MySQLHelper();
            return _MySQLHelper.ExecuteNonQuery(Constant.ConnectionString, System.Data.CommandType.Text, cmdText, cmdParms);
        }
        #endregion

        #region 执行Sql语句
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public long Inster(string cmdText, params MySqlParameter[] cmdParms)
        {
            MySQLHelper _MySQLHelper = new MySQLHelper();
            return _MySQLHelper.Inster(Constant.ConnectionString, System.Data.CommandType.Text, cmdText, cmdParms);
        }
        #endregion

        #region 执行Sql
        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public List<T> ExecuteQuery<T>(string cmdText, params MySqlParameter[] cmdParms)
        {
            List<T> result = new List<T>();
            //执行sql语句
            MySQLHelper _MySQLHelper = new MySQLHelper();
            DataSet returnData = _MySQLHelper.ExecuteDataset(Constant.ConnectionString, System.Data.CommandType.Text, cmdText, cmdParms);
            if (returnData == null || returnData.Tables.Count == 0)
            {
                return result;
            }
            //获取数据集合
            DataTable DT = returnData.Tables[0];
            if (DT.Rows.Count == 0)
            {
                return result;
            }
            foreach (DataRow dr in DT.Rows)
            {
                result.Add(DataRowToObject<T>(dr));
            }
            return result;
        }
        #endregion

        #region ExecuteScalar返回结果集中的第一行第一列
        /// <summary>
        ///  ExecuteScalar返回结果集中的第一行第一列
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText, params MySqlParameter[] cmdParms)
        {
            MySQLHelper _MySQLHelper = new MySQLHelper();
            return _MySQLHelper.ExecuteScalar(Constant.ConnectionString, System.Data.CommandType.Text, cmdText, cmdParms);
        }
        #endregion

        #region 转换数据行
        /// <summary>
        /// 转换数据行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        private T DataRowToObject<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    if (prop == null)
                    {
                        continue;
                    }
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value.ConvertTo(prop.PropertyType), null);
                    }
                    catch (Exception ex)
                    {  //You can log something here     
                        LogFactory _LogFactory = new LogFactory(this.GetType());
                        _LogFactory.CreateLog(LogType.Error, "转换数据行", "DataRowToObject", ex);
                    }
                }
            }
            return obj;
        }
        #endregion

    }
    public static class ConvertionExtensions
    {
        public static object ConvertTo(this object value, Type convertsionType)
        {
            //判断convertsionType类型是否为泛型，因为nullable是泛型类,
            if (convertsionType.IsGenericType &&
                //判断convertsionType是否为nullable泛型类
                convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.ToString().Length == 0)
                {
                    return null;
                }

                //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                NullableConverter nullableConverter = new NullableConverter(convertsionType);
                //将convertsionType转换为nullable对的基础基元类型
                convertsionType = nullableConverter.UnderlyingType;
            }
            if (convertsionType.ToString().ToLower() == "system.guid")
            {
                return new Guid((string)value);
            }
            return Convert.ChangeType(value, convertsionType);
        }

    }

}
