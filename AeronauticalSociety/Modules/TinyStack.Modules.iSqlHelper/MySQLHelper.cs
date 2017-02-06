using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace TinyStack.Modules.iSqlHelper
{
    /// <summary>
    /// 基于MySQL的数据层基类
    /// </summary>
    public class MySQLHelper
    {
        #region 数据库连接字符串

        //public  readonly string DBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString();

        public readonly string DBConnectionString = "Server=localhost;DataBase=menagerie;Uid=de.cel;Pwd=de.cel";
        #endregion

        #region 私有构造函数和方法
        /// <summary>
        /// Command预处理
        /// </summary>
        /// <param name="conn">MySqlConnection对象</param>
        /// <param name="trans">MySqlTransaction对象，可为null</param>
        /// <param name="cmd">MySqlCommand对象</param>
        /// <param name="cmdType">CommandType，存储过程或命令行</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组，可为null</param>
        private void PrepareCommand(MySqlConnection conn, MySqlTransaction trans, MySqlCommand cmd, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region ExecuteNonQuery命令
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组</param>
        /// <returns>返回受引响的记录行数</returns>
        public int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                var lastKey = cmd.LastInsertedId;
                cmd.Parameters.Clear();
                return val;
            }
        }

        public long Inster(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {

            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                var lastKey = cmd.LastInsertedId;
                cmd.Parameters.Clear();
                return lastKey;
            }
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="conn">Connection对象</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组</param>
        /// <returns>返回受引响的记录行数</returns>
        public int ExecuteNonQuery(MySqlConnection conn, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="trans">MySqlTransaction对象</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组</param>
        /// <returns>返回受引响的记录行数</returns>
        public int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(trans.Connection, trans, cmd, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        #endregion

        #region ExecuteDataSet方法
        /// <summary>
        /// 执行命令或存储过程，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程或SQL语句)</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组(可为null值)</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataset(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                cmd.Parameters.Clear();
                return ds;
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
        public DataSet ExecuteDataSet(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            // ExecuteDataset #5
            if (connection == null) throw new ArgumentNullException("connection");
            // 预处理
            MySqlCommand cmd = new MySqlCommand();
            bool mustCloseConnection = false;
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
                foreach (MySqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
            #endregion
            // 创建SqlDataAdapter和DataSet.
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
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

        #region ExecuteReader数据阅读器
        /// <summary>
        /// 执行命令或存储过程，返回MySqlDataReader对象
        /// 注意MySqlDataReader对象使用完后必须Close以释放MySqlConnection资源
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组</param>
        /// <returns></returns>
        public MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dr;
            }
            catch
            {
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
        public MySqlDataReader ExecuteReader(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            // ExecuteReader #1
            if (connection == null) throw new ArgumentNullException("connection");
            bool mustCloseConnection = false;
            // 创建命令
            MySqlCommand cmd = new MySqlCommand();
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
                    foreach (MySqlParameter parm in commandParameters)
                        cmd.Parameters.Add(parm);
                }
                #endregion
                // 创建数据阅读器
                MySqlDataReader dataReader = cmd.ExecuteReader();

                // 清除参数,以便再次使用..
                bool canClear = true;
                foreach (MySqlParameter commandParameter in cmd.Parameters)
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

        #region ExecuteScalar返回结果集中的第一行第一列

        /// <summary>
        /// 执行命令，返回第一行第一列的值
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组</param>
        /// <returns>返回Object对象</returns>
        public object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(connection, null, cmd, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行命令，返回第一行第一列的值
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组</param>
        /// <returns>返回Object对象</returns>
        public object ExecuteScalar(MySqlConnection conn, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
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
        public bool ExecuteNonQuery(MySqlConnection connection, List<string> SqlTexts, List<MySqlParameter[]> commandParameters, bool IsTransaction)
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            if (IsTransaction)
            {
                MySqlTransaction tran = null;
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
