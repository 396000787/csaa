using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;

namespace TinyStack.Modules.iSqlHelper
{
    /// <summary>
    /// AccessHelper 的摘要说明
    /// </summary>
    public class AccessHelper
    {
        //数据库连接字符串
        //public static readonly string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Request.PhysicalApplicationPath + System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static readonly string connectionString = "";// @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["accessconstr"].ToString());

        #region 私有构造函数和方法
        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">Sql连接</param>
        /// <param name="trans">Sql事务</param>
        /// <param name="cmdText">命令文本,例如：Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, string cmdText, OleDbParameter[] cmdParms)
        {
            //判断连接的状态。如果是关闭状态，则打开
            if (conn.State != ConnectionState.Open)
                conn.Open();
            //cmd属性赋值
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //是否需要用到事务处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            //添加cmd需要的存储过程参数
            if (cmdParms != null)
            {
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region ExecuteNonQuery命令
        /// <summary>
        /// 给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 用现有的数据库连接执行一个sql命令（不返回数据集）
        /// </summary>
        /// <remarks>
        ///举例: 
        /// int result = ExecuteNonQuery(connString, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">一个现有的数据库连接</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(OleDbConnection connection, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, connection, null, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        /// <summary>
        ///使用现有的SQL事务执行一个sql命令（不返回数据集）
        /// </summary>
        /// <remarks>
        ///举例: 
        /// int result = ExecuteNonQuery(trans, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个现有的事务</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(OleDbTransaction trans, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion ExecuteNonQuery命令

        #region ExecuteDataset方法
        /// <summary>
        /// 返回一个DataSet数据集
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的数据集</returns>
        public static DataSet ExecuteDataset(string connectionString, string cmdText, params OleDbParameter[] commandParameters)
        {
            //创建一个SqlCommand对象，并对其进行初始化
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdText, commandParameters);
                //创建SqlDataAdapter对象以及DataSet
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    //填充ds
                    da.Fill(ds);
                    // 清除cmd的参数集合 
                    cmd.Parameters.Clear();
                    //返回ds
                    return ds;
                }
                catch
                {
                    //关闭连接，抛出异常
                    conn.Close();
                    throw;
                }
            }
        }
        /// <summary>
        /// 返回一个DataSet数据集
        /// </summary>
        /// <param name="connection">一个有效的数据连接</param>
        /// <param name="commandText">sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的数据集</returns>
        public static DataSet ExecuteDataset(OleDbConnection connection, string commandText, params OleDbParameter[] commandParameters)
        {
            // ExecuteDataset #5
            if (connection == null) throw new ArgumentNullException("connection");
            // 预处理
            OleDbCommand cmd = new OleDbCommand();
            bool mustCloseConnection = false;
            //PrepareCommand(cmd, connection, null, commandText, commandParameters, out mustCloseConnection);
            #region PrepareCommand
            //判断连接的状态。如果是关闭状态，则打开
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            //cmd属性赋值
            cmd.Connection = connection;
            cmd.CommandText = commandText;

            cmd.CommandType = CommandType.Text;
            //添加cmd需要的存储过程参数
            if (commandParameters != null)
            {
                foreach (OleDbParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
            #endregion
            // 创建SqlDataAdapter和DataSet.
            using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                // 填充DataSet.
                da.Fill(ds);
                cmd.Parameters.Clear();
                if (mustCloseConnection)
                    connection.Close();
                return ds;
            }
        }
        #endregion

        #region ExecuteReader 数据阅读器
        /// <summary>
        /// 用执行的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <remarks>
        /// 举例: 
        /// OleDbDataReader r = ExecuteReader(connString, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public static OleDbDataReader ExecuteReader(string connectionString, string cmdText, params OleDbParameter[] commandParameters)
        {
            //创建一个SqlCommand对象
            OleDbCommand cmd = new OleDbCommand();
            //创建一个SqlConnection对象
            OleDbConnection conn = new OleDbConnection(connectionString);
            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            //因此commandBehaviour.CloseConnection 就不会执行
            try
            {
                //调用 PrepareCommand 方法，对 SqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdText, commandParameters);
                //调用 SqlCommand 的 ExecuteReader 方法
                OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清除参数
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //关闭连接，抛出异常
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// 执行指定数据库连接对象的数据阅读器.
        /// </summary>
        /// <remarks>
        /// 如果是SqlServerHelper打开连接,当连接关闭DataReader也将关闭.
        /// 如果是调用都打开连接,DataReader由调用都管理.
        /// </remarks>
        /// <param name="connection">一个有效的数据库连接对象</param>
        /// <param name="transaction">一个有效的事务,或者为 'null'</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或T-SQL语句</param>
        /// <param name="commandParameters">SqlParameters参数数组,如果没有参数则为'null'</param>
        /// <param name="connectionOwnership">标识数据库连接对象是由调用者提供还是由SqlServerHelper提供</param>
        /// <returns>返回包含结果集的SqlDataReader</returns>
        public static OleDbDataReader ExecuteReader(OleDbConnection connection, string commandText, params OleDbParameter[] commandParameters)
        {
            // ExecuteReader #1
            if (connection == null) throw new ArgumentNullException("connection");
            bool mustCloseConnection = false;
            // 创建命令
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                //PrepareCommand(cmd, connection, commandType, commandText, commandParameters);
                #region PrepareCommand
                //判断连接的状态。如果是关闭状态，则打开
                if (connection.State != ConnectionState.Open)
                {
                    mustCloseConnection = true;
                    connection.Open();
                }
                else
                {
                    mustCloseConnection = false;
                }
                //cmd属性赋值
                cmd.Connection = connection;
                cmd.CommandText = commandText;

                cmd.CommandType = CommandType.Text;
                //添加cmd需要的存储过程参数
                if (commandParameters != null)
                {
                    foreach (OleDbParameter parm in commandParameters)
                        cmd.Parameters.Add(parm);
                }
                #endregion
                // 创建数据阅读器
                OleDbDataReader dataReader = cmd.ExecuteReader();
               
                // 清除参数,以便再次使用..
                bool canClear = true;
                foreach (OleDbParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }
                if (canClear)
                {
                    cmd.Parameters.Clear();
                }
                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }
        #endregion

        #region ExecuteScalar 返回结果集中的第一行第一列
        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        ///例如: 
        /// Object obj = ExecuteScalar(connString, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(string connectionString, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 用指定的数据库连接执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        /// 例如: 
        /// Object obj = ExecuteScalar(connString, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">一个存在的数据库连接</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(OleDbConnection connection, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, connection, null, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion

        #region 批量执行SQL语句
        /// <summary>
        /// 批量执行SQL语句
        /// </summary>
        /// <param name="connection">数据连接</param>
        /// <param name="SqlTexts">SQL语句数组</param>
        /// <param name="commandParameters">SQL参数对象数组</param>
        /// <param name="IsTransaction">是否使用事务</param>
        /// <returns>是否执行成功</returns>
        public static bool ExecuteNonQuery(OleDbConnection connection, List<string> SqlTexts, List<OleDbParameter[]> commandParameters, bool IsTransaction)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            bool mustCloseConnection = false;
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;
            if (IsTransaction)
            {
                OleDbTransaction tran = null;
                //cmd.Transaction = tran;
                try
                {
                    tran = connection.BeginTransaction();
                    cmd.Transaction = tran;

                    Int32 count = SqlTexts.Count;
                    for (Int32 i = 0; i < count; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = SqlTexts[i];
                        if (commandParameters != null)
                        {
                            cmd.Parameters.AddRange(commandParameters[i]);
                        }
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    cmd.Dispose();
                    if (mustCloseConnection)
                        connection.Close();
                }
                return true;
            }
            else
            {
                try
                {
                    //cmd.Connection = connection;
                    Int32 count = SqlTexts.Count;
                    for (Int32 i = 0; i < count; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = SqlTexts[i];
                        try
                        {
                            if (commandParameters != null)
                            {
                                cmd.Parameters.AddRange(commandParameters[i]);
                            }
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception)
                        { }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

    }
}
