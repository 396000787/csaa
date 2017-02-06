using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace TinyStack.Modules.iSqlHelper
{
    /// <summary>
    /// A helper class used to execute queries against an Oracle database
    /// </summary>
    public abstract class OracleHelper
    {
        // Read the connection strings from the configuration file
        public static readonly string ConnectionStringLocalTransaction ;//= ConfigurationManager.ConnectionStrings["OraConnString1"].ConnectionString;
        public static readonly string ConnectionStringInventoryDistributedTransaction ;//= ConfigurationManager.ConnectionStrings["OraConnString2"].ConnectionString;
        public static readonly string ConnectionStringOrderDistributedTransaction ;//= ConfigurationManager.ConnectionStrings["OraConnString3"].ConnectionString;
        public static readonly string ConnectionStringProfile;// = ConfigurationManager.ConnectionStrings["OraProfileConnString"].ConnectionString;
        public static readonly string ConnectionStringMembership ;//= ConfigurationManager.ConnectionStrings["OraMembershipConnString"].ConnectionString;

        #region 私有构造函数和方法
        /// <summary>
        /// Internal function to prepare a command for execution by the database
        /// </summary>
        /// <param name="cmd">Existing command object</param>
        /// <param name="conn">Database connection object</param>
        /// <param name="trans">Optional transaction object</param>
        /// <param name="cmdType">Command type, e.g. stored procedure</param>
        /// <param name="cmdText">Command test</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {

            //Open the connection if required
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //Set up the command
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //Bind it to the transaction if it exists
            if (trans != null)
                cmd.Transaction = trans;

            // Bind the parameters passed in
            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region ExecuteNonQuery命令
        /// <summary>
        /// 用现有的数据库连接执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            // Create a new Oracle command
            OracleCommand cmd = new OracleCommand();

            //Create a connection
            using (OracleConnection connection = new OracleConnection(connectionString))
            {

                //Prepare the command
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                //Execute the command
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行一个OracleCommand对现有数据库事务（不返回结果集）
        /// 使用提供的参数
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="trans">一个现有的数据库事务</param>
        /// <param name="commandType">CommandType（存储过程，SQL文本等）</param>
        /// <param name="commandText">存储过程的名称或SQL命令</param>
        /// <param name="commandParameters">OracleParamters用于执行的参数数组</param>
        /// <returns>命令所影响的行数</returns>
        public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行一个OracleCommand对现有的数据库连接
        /// 使用提供的参数
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="conn">一个现有的数据库连接</param>
        /// <param name="commandType">CommandType（存储过程，SQL文本等）</param>
        /// <param name="commandText">存储过程的名称或SQL命令</param>
        /// <param name="commandParameters">OracleParamters用于执行的参数数组</param>
        /// <returns>命令所影响的行数</returns>
        public static int ExecuteNonQuery(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion

        #region ExecuteDataset方法
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">存储过程的名称或SQL命令</param>
        /// <param name="cmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="cmdParms">OracleParamters用于执行的参数数组</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataset(string SQLString, CommandType cmdType, params OracleParameter[] cmdParms)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionStringMembership))
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, null, cmdType, SQLString, cmdParms);
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.OracleClient.OracleException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                    return ds;
                }
            }
        }
        /// <summary>
        /// 执行指定数据库连接对象的命令,指定存储过程参数,返回DataSet.
        /// </summary>
        /// <remarks>
        /// 示例:
        /// DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">一个有效的数据库连接对象</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或T-SQL语句</param>
        /// <param name="commandParameters">SqlParamter参数数组</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            // ExecuteDataset #5
            if (connection == null) throw new ArgumentNullException("connection");
            // 预处理
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            //PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
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

            cmd.CommandType = commandType;
            //添加cmd需要的存储过程参数
            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
            #endregion
            // 创建SqlDataAdapter和DataSet.
            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
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
        /// 执行查询语句，返回结果集 ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="connString">一个有效的数据库连接字符串</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">存储过程的名称或SQL命令</param>
        /// <param name="commandParameters">OracleParamters用于执行的参数数组</param>
        /// <returns>包含结果集的OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            //Create the command and connection
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                //Prepare the command to execute
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //Execute the query, stating that the connection should close when the resulting datareader has been read
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                //If an error occurs close the connection as the reader will not be used and we expect it to close the connection
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
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            // ExecuteReader #1
            if (connection == null) throw new ArgumentNullException("connection");
            bool mustCloseConnection = false;
            // 创建命令
            OracleCommand cmd = new OracleCommand();
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

                cmd.CommandType = commandType;
                //添加cmd需要的存储过程参数
                if (commandParameters != null)
                {
                    foreach (OracleParameter parm in commandParameters)
                        cmd.Parameters.Add(parm);
                }
                #endregion
                // 创建数据阅读器
                OracleDataReader dataReader = cmd.ExecuteReader();

                // 清除参数,以便再次使用..
                bool canClear = true;
                foreach (OracleParameter commandParameter in cmd.Parameters)
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
        /// 执行一个OracleCommand，返回结果集中的第一行第一列
        /// 使用提供的参数
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandType">CommandType（存储过程，SQL文本等）</param>
        /// <param name="commandText">存储过程的名称或SQL命令</param>
        /// <param name="commandParameters">OracleParamters用于执行的参数数组</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行指定数据库事务的命令,返回结果集中的第一行第一列.
        /// </summary>
        /// <param name="transaction">一个有效的连接事务</param>
        /// <param name="commandType">CommandType（存储过程，SQL文本等）</param>
        /// <param name="commandText">存储过程的名称或SQL命令</param>
        /// <param name="commandParameters">OracleParamters用于执行的参数数组</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked    or commited, please    provide    an open    transaction.", "transaction");

            // Create a    command    and    prepare    it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
            // Execute the command & return    the    results
            object retval = cmd.ExecuteScalar();
            // Detach the SqlParameters    from the command object, so    they can be    used again
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// 执行一个OracleCommand对现有的数据库连接，返回的第一条记录第一列
        /// 使用提供的参数
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="conn">一个现有的数据库连接</param>
        /// <param name="commandType">CommandType（存储过程，SQL文本等）</param>
        /// <param name="commandText">存储过程的名称或SQL命令</param>
        /// <param name="commandParameters">存储过程的名称或SQL命令</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(OracleConnection connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, connectionString, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion

        #region Converter转换类
        /// <summary>
        /// 转换器使用布尔数据类型与Oracle
        /// </summary>
        /// <param name="value">要转换的bool值</param>
        /// <returns>转换后的结果,Y:true;N:false</returns>
        public static string OraBit(bool value)
        {
            if (value)
                return "Y";
            else
                return "N";
        }

        /// <summary>
        /// 将Oracle的值转换为bool类型
        /// </summary>
        /// <param name="value">要转换的值Y或者N</param>
        /// <returns>转换后的结果,true:Y;false:N</returns>
        public static bool OraBool(string value)
        {
            if (value.Equals("Y"))
                return true;
            else
                return false;
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
        public static bool ExecuteNonQuery(OracleConnection connection, List<string> SqlTexts, List<OracleParameter[]> commandParameters, bool IsTransaction)
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
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            if (IsTransaction)
            {
                OracleTransaction tran = null;
                cmd.Transaction = tran;
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
