using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using MySql.Data.MySqlClient;
using System.Data.EntityClient;

namespace TinyStack.Modules.iSqlHelper
{
    /// <summary>
    /// SqlHelper封装类
    /// </summary>
    public class SqlHelper
    {
        #region 属性
        /// <summary>
        /// 数据库连接池
        /// </summary>
        private ConnectionPool connectionPool = null;

        public ConnectionPool ConnectionPool
        {
            get { return connectionPool; }
            set { connectionPool = value; }
        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string connectionString;
        /// <summary>
        /// 是否使用连接池
        /// </summary>
        private bool isConnectionPool = false;
        private ConnTypeEnum connType = ConnTypeEnum.SqlClient;
        #endregion

        #region 构造方法
        /// <summary>
        /// 构造方法（根据连接字符串创建连接池，默认ConnTypeEnum=SqlServer,maxConnection=200,minConnection=30,seepConnection=10,keepConnection=5,keepRealConnection=5）
        /// </summary>
        /// <param name="_ConnectionString">数据库连接字符串</param>
        /// <param name="_IsConnectionPool">是否使用连接池</param>
        /// <param name="_ConnType">数据库类型</param>
        public SqlHelper(String _ConnectionString, bool _IsConnectionPool = false, ConnTypeEnum _ConnType = ConnTypeEnum.SqlClient)
        {
            //保存连接字符串
            connectionString = _ConnectionString;
            //保存是否使用连接池
            isConnectionPool = _IsConnectionPool;
            //保存数据库类型
            connType = _ConnType;
            if (_IsConnectionPool)//如果使用连接池
            {
                //实例化连接池
                this.connectionPool = new ConnectionPool(_ConnectionString, connType);
            }
        }
        //public SqlHelper(String _connectionString, ConnTypeEnum cte)
        //{
        //    connectionString = _connectionString;
        //    this.connectionPool = new ConnectionPool(_connectionString, cte);
        //}
        /// <summary>
        /// 构造方法（根据连接字符串创建连接池，默认ConnTypeEnum=SqlServer,maxConnection=200,minConnection=30,seepConnection=10,keepConnection=5,keepRealConnection=5）
        /// </summary>
        /// <param name="_ConnectionString">数据库连接字符串</param>
        /// <param name="maxConnection">最大连接数</param>
        /// <param name="minConnection">最小连接数</param>
        /// <param name="_ConnType">数据库类型</param>
        public SqlHelper(String _ConnectionString, int maxConnection, int minConnection, ConnTypeEnum _ConnType = ConnTypeEnum.SqlClient)
        {
            //保存连接字符串
            connectionString = _ConnectionString;
            //保存数据库类型
            connType = _ConnType;
            //使用连接池
            isConnectionPool = true;
            //实例化连接池
            this.connectionPool = new ConnectionPool(_ConnectionString, connType, maxConnection, minConnection);
        }
        /// <summary>
        /// 构造方法（根据连接字符串创建连接池，默认SqlServer,）
        /// </summary>
        /// <param name="_ConnectionString">数据库连接字符串</param>
        /// <param name="cte">数据库连接类型</param>
        /// <param name="maxConnection">最大连接数，最大可以创建的连接数目</param>
        /// <param name="minConnection">最小连接数</param>
        /// <param name="seepConnection">每次创建连接的连接数</param>
        /// <param name="keepConnection">保留连接数，当空闲连接不足该数值时，连接池将创建seepConnection个连接</param>
        /// <param name="keepRealConnection">当空闲的实际连接不足该值时创建连接，直到达到最大连接数</param>
        public SqlHelper(String _ConnectionString, int maxConnection, int minConnection, int seepConnection, int keepConnection, int keepRealConnection, ConnTypeEnum _ConnType = ConnTypeEnum.SqlClient)
        {
            //保存连接字符串
            connectionString = _ConnectionString;
            //保存数据库类型
            connType = _ConnType;
            //使用连接池
            isConnectionPool = true;
            //实例化连接池
            this.connectionPool = new ConnectionPool(_ConnectionString, connType, maxConnection, minConnection, seepConnection, keepConnection, keepRealConnection);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connectionPool">数据库连接池</param>
        public SqlHelper(ConnectionPool _ConnectionPool)
        {
            //保存数据库类型
            connType = _ConnectionPool.ConnectionType;
            //使用连接池
            isConnectionPool = true;
            //保存连接池
            this.connectionPool = _ConnectionPool;
        }
        #endregion

        #region ExecuteNonQuery命令

        #region 参数：SQL语句
        /// <summary>
        /// 执行指定SQL语句或存储过程
        /// </summary>
        /// <param name="SqlText">存储过程名称或SQL语句</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="IsTransaction">是否使用事务</param>
        /// <returns>返回命令影响的行数</returns>
        public int ExecuteNonQuery(String SqlText, CommandType CmdType = CommandType.Text, bool IsTransaction = false)
        {
            Guid _key = Guid.NewGuid();
            int _count = 0;
            try
            {
                switch (connType)
                {
                    #region SqlClient
                    case ConnTypeEnum.SqlClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                                {
                                    _count = SqlServerHelper.ExecuteNonQuery(_SqlConnection, CmdType, SqlText);
                                }
                            }
                            else//如果使用事务
                            {
                                using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                                {
                                    _SqlConnection.Open();
                                    SqlTransaction tran = _SqlConnection.BeginTransaction();
                                    try
                                    {
                                        _count = SqlServerHelper.ExecuteNonQuery(tran, CmdType, SqlText);
                                        tran.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        tran.Rollback();
                                    }
                                    finally
                                    {
                                        tran.Dispose();
                                        _SqlConnection.Close();
                                    }
                                }
                            }
                        }
                        else//如果使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                                _count = SqlServerHelper.ExecuteNonQuery(_SqlConnection, CmdType, SqlText);
                                connectionPool.DisposeConnection(_key);
                            }
                            else//如果使用事务
                            {
                                SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                                SqlTransaction tran = _SqlConnection.BeginTransaction();
                                try
                                {
                                    _count = SqlServerHelper.ExecuteNonQuery(tran, CmdType, SqlText);
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                }
                                finally
                                {
                                    tran.Dispose();
                                    connectionPool.DisposeConnection(_key);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region OleDb
                    case ConnTypeEnum.OleDb:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                                {
                                    _count = AccessHelper.ExecuteNonQuery(_OleDbConnection, SqlText);
                                }
                            }
                            else//如果使用事务
                            {
                                using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                                {
                                    _OleDbConnection.Open();
                                    OleDbTransaction tran = _OleDbConnection.BeginTransaction();
                                    try
                                    {
                                        _count = AccessHelper.ExecuteNonQuery(tran, SqlText);
                                        tran.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        tran.Rollback();
                                    }
                                    finally
                                    {
                                        tran.Dispose();
                                        _OleDbConnection.Close();
                                    }
                                }
                            }
                        }
                        else//如果使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                                _count = AccessHelper.ExecuteNonQuery(_OleDbConnection, SqlText);
                                connectionPool.DisposeConnection(_key);
                            }
                            else//如果使用事务
                            {
                                OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                                OleDbTransaction tran = _OleDbConnection.BeginTransaction();
                                try
                                {
                                    _count = AccessHelper.ExecuteNonQuery(tran, SqlText);
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                }
                                finally
                                {
                                    tran.Dispose();
                                    connectionPool.DisposeConnection(_key);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region OracleClient
                    case ConnTypeEnum.OracleClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                                {
                                    _count = OracleHelper.ExecuteNonQuery(_OracleConnection, CmdType, SqlText);
                                }
                            }
                            else//如果使用事务
                            {
                                using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                                {
                                    _OracleConnection.Open();
                                    OracleTransaction tran = _OracleConnection.BeginTransaction();
                                    try
                                    {
                                        _count = OracleHelper.ExecuteNonQuery(tran, CmdType, SqlText);
                                        tran.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        tran.Rollback();
                                    }
                                    finally
                                    {
                                        tran.Dispose();
                                        _OracleConnection.Close();
                                    }
                                }
                            }
                        }
                        else//如果使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                                _count = OracleHelper.ExecuteNonQuery(_OracleConnection, CmdType, SqlText);
                                connectionPool.DisposeConnection(_key);
                            }
                            else//如果使用事务
                            {
                                OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                                OracleTransaction tran = _OracleConnection.BeginTransaction();
                                try
                                {
                                    _count = OracleHelper.ExecuteNonQuery(tran, CmdType, SqlText);
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                }
                                finally
                                {
                                    tran.Dispose();
                                    connectionPool.DisposeConnection(_key);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region MySqlClient
                    case ConnTypeEnum.MySqlClient:
                        MySQLHelper _MySQLHelper = new MySQLHelper();
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                                {
                                    _count = _MySQLHelper.ExecuteNonQuery(_MySqlConnection, CmdType, SqlText);
                                }
                            }
                            else//如果使用事务
                            {
                                using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                                {
                                    _MySqlConnection.Open();
                                    MySqlTransaction tran = _MySqlConnection.BeginTransaction();
                                    try
                                    {
                                        _count = _MySQLHelper.ExecuteNonQuery(tran, CmdType, SqlText);
                                        tran.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        tran.Rollback();
                                    }
                                    finally
                                    {
                                        tran.Dispose();
                                        _MySqlConnection.Close();
                                    }
                                }
                            }
                        }
                        else//如果使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                                _count = _MySQLHelper.ExecuteNonQuery(_MySqlConnection, CmdType, SqlText);
                                connectionPool.DisposeConnection(_key);
                            }
                            else//如果使用事务
                            {
                                MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                                MySqlTransaction tran = _MySqlConnection.BeginTransaction();
                                try
                                {
                                    _count = _MySQLHelper.ExecuteNonQuery(tran, CmdType, SqlText);
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                }
                                finally
                                {
                                    tran.Dispose();
                                    connectionPool.DisposeConnection(_key);
                                }
                            }
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            return _count;
        }
        /// <summary>
        /// 执行指定SQL语句或存储过程
        /// </summary>
        /// <param name="SqlNum">存储过程名称或SQL语句的序号</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="IsTransaction">是否使用事务</param>
        /// <returns>返回命令影响的行数</returns>
        public int ExecuteNonQueryByNum(String SqlNum, CommandType CmdType = CommandType.Text, bool IsTransaction = false)
        {
            int _count = 0;
            try
            {
                //实例化获取SQL语句帮助类
                SQLCodeHelp _SQLCodeHelp = new SQLCodeHelp();
                //根据序号获取SQL语句
                string _SqlText = _SQLCodeHelp.GetSQLCode(SqlNum);
                //判断SQL语句是否为空
                if (!string.IsNullOrEmpty(_SqlText))
                {
                    //执行SQL语句
                    _count = ExecuteNonQuery(_SqlText, CmdType, IsTransaction);
                }
            }
            catch (Exception ex)
            {

            }
            return _count;
        }
        #endregion

        #region 参数：SQL语句、执行命令所用参数的集合
        /// <summary>
        /// 执行指定SQL语句或存储过程,提供参数
        /// </summary>
        /// <param name="SqlText">存储过程名称或SQL语句</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="IsTransaction">是否使用事务</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回命令影响的行数</returns>
        public int ExecuteNonQuery(String SqlText, CommandType CmdType = CommandType.Text, bool IsTransaction = false, params object[] commandParameters)
        {
            Guid _key = Guid.NewGuid();
            int _count = 0;
            try
            {
                switch (connType)
                {
                    #region SqlClient
                    case ConnTypeEnum.SqlClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                                {
                                    _count = SqlServerHelper.ExecuteNonQuery(_SqlConnection, CmdType, SqlText, (SqlParameter[])commandParameters);
                                }
                            }
                            else//如果使用事务
                            {
                                using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                                {
                                    _SqlConnection.Open();
                                    SqlTransaction tran = _SqlConnection.BeginTransaction();
                                    try
                                    {
                                        _count = SqlServerHelper.ExecuteNonQuery(tran, CmdType, SqlText, (SqlParameter[])commandParameters);
                                        tran.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        tran.Rollback();
                                    }
                                    finally
                                    {
                                        tran.Dispose();
                                        _SqlConnection.Close();
                                    }
                                }
                            }
                        }
                        else//如果使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                                _count = SqlServerHelper.ExecuteNonQuery(_SqlConnection, CmdType, SqlText, (SqlParameter[])commandParameters);
                                connectionPool.DisposeConnection(_key);
                            }
                            else//如果使用事务
                            {
                                SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                                SqlTransaction tran = _SqlConnection.BeginTransaction();
                                try
                                {
                                    _count = SqlServerHelper.ExecuteNonQuery(tran, CmdType, SqlText, (SqlParameter[])commandParameters);
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                }
                                finally
                                {
                                    tran.Dispose();
                                    connectionPool.DisposeConnection(_key);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region OleDb
                    case ConnTypeEnum.OleDb:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                                {
                                    _count = AccessHelper.ExecuteNonQuery(_OleDbConnection, SqlText, (OleDbParameter[])commandParameters);
                                }
                            }
                            else//如果使用事务
                            {
                                using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                                {
                                    _OleDbConnection.Open();
                                    OleDbTransaction tran = _OleDbConnection.BeginTransaction();
                                    try
                                    {
                                        _count = AccessHelper.ExecuteNonQuery(tran, SqlText, (OleDbParameter[])commandParameters);
                                        tran.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        tran.Rollback();
                                    }
                                    finally
                                    {
                                        tran.Dispose();
                                        _OleDbConnection.Close();
                                    }
                                }
                            }
                        }
                        else//如果使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                                _count = AccessHelper.ExecuteNonQuery(_OleDbConnection, SqlText, (OleDbParameter[])commandParameters);
                                connectionPool.DisposeConnection(_key);
                            }
                            else//如果使用事务
                            {
                                OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                                OleDbTransaction tran = _OleDbConnection.BeginTransaction();
                                try
                                {
                                    _count = AccessHelper.ExecuteNonQuery(tran, SqlText, (OleDbParameter[])commandParameters);
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                }
                                finally
                                {
                                    tran.Dispose();
                                    connectionPool.DisposeConnection(_key);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region OracleClient
                    case ConnTypeEnum.OracleClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                                {
                                    _count = OracleHelper.ExecuteNonQuery(_OracleConnection, CmdType, SqlText, (OracleParameter[])commandParameters);
                                }
                            }
                            else//如果使用事务
                            {
                                using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                                {
                                    _OracleConnection.Open();
                                    OracleTransaction tran = _OracleConnection.BeginTransaction();
                                    try
                                    {
                                        _count = OracleHelper.ExecuteNonQuery(tran, CmdType, SqlText, (OracleParameter[])commandParameters);
                                        tran.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        tran.Rollback();
                                    }
                                    finally
                                    {
                                        tran.Dispose();
                                        _OracleConnection.Close();
                                    }
                                }
                            }
                        }
                        else//如果使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                                _count = OracleHelper.ExecuteNonQuery(_OracleConnection, CmdType, SqlText, (OracleParameter[])commandParameters);
                                connectionPool.DisposeConnection(_key);
                            }
                            else//如果使用事务
                            {
                                OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                                OracleTransaction tran = _OracleConnection.BeginTransaction();
                                try
                                {
                                    _count = OracleHelper.ExecuteNonQuery(tran, CmdType, SqlText, (OracleParameter[])commandParameters);
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                }
                                finally
                                {
                                    tran.Dispose();
                                    connectionPool.DisposeConnection(_key);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region MySqlClient
                    case ConnTypeEnum.MySqlClient:
                        MySQLHelper _MySQLHelper = new MySQLHelper();
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                                {
                                    _count = _MySQLHelper.ExecuteNonQuery(_MySqlConnection, CmdType, SqlText, (MySqlParameter[])commandParameters);
                                }
                            }
                            else//如果使用事务
                            {
                                using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                                {
                                    _MySqlConnection.Open();
                                    MySqlTransaction tran = _MySqlConnection.BeginTransaction();
                                    try
                                    {
                                        _count = _MySQLHelper.ExecuteNonQuery(tran, CmdType, SqlText, (MySqlParameter[])commandParameters);
                                        tran.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        tran.Rollback();
                                    }
                                    finally
                                    {
                                        tran.Dispose();
                                        _MySqlConnection.Close();
                                    }
                                }
                            }
                        }
                        else//如果使用连接池
                        {
                            if (!IsTransaction)//如果不使用事务
                            {
                                MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                                _count = _MySQLHelper.ExecuteNonQuery(_MySqlConnection, CmdType, SqlText, (MySqlParameter[])commandParameters);
                                connectionPool.DisposeConnection(_key);
                            }
                            else//如果使用事务
                            {
                                MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                                MySqlTransaction tran = _MySqlConnection.BeginTransaction();
                                try
                                {
                                    _count = _MySQLHelper.ExecuteNonQuery(tran, CmdType, SqlText, (MySqlParameter[])commandParameters);
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                }
                                finally
                                {
                                    tran.Dispose();
                                    connectionPool.DisposeConnection(_key);
                                }
                            }
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            return _count;
        }
        /// <summary>
        /// 执行指定SQL语句或存储过程,提供参数
        /// </summary>
        /// <param name="SqlNum">存储过程名称或SQL语句的序号</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="IsTransaction">是否使用事务</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回命令影响的行数</returns>
        public int ExecuteNonQueryByNum(String SqlNum, CommandType CmdType = CommandType.Text, bool IsTransaction = false, params object[] commandParameters)
        {
            int _count = 0;
            try
            {
                //实例化获取SQL语句帮助类
                SQLCodeHelp _SQLCodeHelp = new SQLCodeHelp();
                //根据序号获取SQL语句
                string _SqlText = _SQLCodeHelp.GetSQLCode(SqlNum);
                //判断SQL语句是否为空
                if (!string.IsNullOrEmpty(_SqlText))
                {
                    //执行SQL语句
                    _count = ExecuteNonQuery(_SqlText, CmdType, IsTransaction, commandParameters);
                }
            }
            catch (Exception ex)
            {

            }
            return _count;
        }
        #endregion

        #region 参数：SQL语句数组、执行命令所用参数的集合
        /// <summary>
        /// 批量执行SQl语句，提供参数
        /// </summary>
        /// <param name="SqlTexts">SQL语句列表</param>
        /// <param name="commandParameters">参数数组列表</param>
        /// <param name="IsTransaction">是否使用事务</param>
        /// <returns>是否执行成功</returns>
        public bool ExecuteNonQuery(List<String> SqlTexts, List<object[]> commandParameters = null, bool IsTransaction = false)
        {
            Guid _key = Guid.NewGuid();
            bool _IsSuccess = false;
            try
            {
                switch (connType)
                {
                    #region SqlClient
                    case ConnTypeEnum.SqlClient:
                        List<SqlParameter[]> SqlParameterParameters = new List<SqlParameter[]>();
                        if (commandParameters != null)
                        {
                            foreach (var item in commandParameters)
                            {
                                SqlParameterParameters.Add((SqlParameter[])item);
                            }
                        }
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                            {
                                _IsSuccess = SqlServerHelper.ExecuteNonQuery(_SqlConnection, SqlTexts, SqlParameterParameters, IsTransaction);
                            }

                        }
                        else//如果使用连接池
                        {
                            SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _IsSuccess = SqlServerHelper.ExecuteNonQuery(_SqlConnection, SqlTexts, SqlParameterParameters, IsTransaction);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OleDb
                    case ConnTypeEnum.OleDb:
                        List<OleDbParameter[]> OleDbParameterParameters = new List<OleDbParameter[]>();
                        if (commandParameters != null)
                        {
                            foreach (var item in commandParameters)
                            {
                                OleDbParameterParameters.Add((OleDbParameter[])item);
                            }
                        }
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                            {
                                _IsSuccess = AccessHelper.ExecuteNonQuery(_OleDbConnection, SqlTexts, OleDbParameterParameters, IsTransaction);
                            }

                        }
                        else//如果使用连接池
                        {
                            OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                            _IsSuccess = AccessHelper.ExecuteNonQuery(_OleDbConnection, SqlTexts, OleDbParameterParameters, IsTransaction);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OracleClient
                    case ConnTypeEnum.OracleClient:
                        List<OracleParameter[]> OracleParameterParameters = new List<OracleParameter[]>();
                        if (commandParameters != null)
                        {
                            foreach (var item in commandParameters)
                            {
                                OracleParameterParameters.Add((OracleParameter[])item);
                            }
                        }
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                            {
                                _IsSuccess = OracleHelper.ExecuteNonQuery(_OracleConnection, SqlTexts, OracleParameterParameters, IsTransaction);
                            }

                        }
                        else//如果使用连接池
                        {
                            OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                            _IsSuccess = OracleHelper.ExecuteNonQuery(_OracleConnection, SqlTexts, OracleParameterParameters, IsTransaction);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region MySqlClient
                    case ConnTypeEnum.MySqlClient:
                        MySQLHelper _MySQLHelper = new MySQLHelper();
                        List<MySqlParameter[]> MySqlParameterParameters = new List<MySqlParameter[]>();
                        if (commandParameters != null)
                        {
                            foreach (var item in commandParameters)
                            {
                                MySqlParameterParameters.Add((MySqlParameter[])item);
                            }
                        }
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                            {
                                _IsSuccess = _MySQLHelper.ExecuteNonQuery(_MySqlConnection, SqlTexts, MySqlParameterParameters, IsTransaction);
                            }

                        }
                        else//如果使用连接池
                        {
                            MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _IsSuccess = _MySQLHelper.ExecuteNonQuery(_MySqlConnection, SqlTexts, MySqlParameterParameters, IsTransaction);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            return _IsSuccess;
        }
        /// <summary>
        /// 批量执行SQl语句，提供参数
        /// </summary>
        /// <param name="SqlNums">SQL语句序号列表</param>
        /// <param name="commandParameters">参数数组列表</param>
        /// <param name="IsTransaction">是否使用事务</param>
        /// <returns>是否执行成功</returns>
        public bool ExecuteNonQueryByNum(List<String> SqlNums, List<object[]> commandParameters, bool IsTransaction = false)
        {
            bool _IsSuccess = false;
            try
            {
                List<String> SqlTexts = new List<String>();
                //实例化获取SQL语句帮助类
                SQLCodeHelp _SQLCodeHelp = new SQLCodeHelp();
                foreach (String SqlNumitem in SqlNums)
                {
                    //根据序号获取SQL语句
                    string _SqlText = _SQLCodeHelp.GetSQLCode(SqlNumitem);
                    if (!string.IsNullOrEmpty(_SqlText))//如果SQL语句不为空
                    {
                        SqlTexts.Add(_SqlText);
                    }
                }

                //判断SQL语句是否为空
                if (SqlTexts.Count > 0)
                {
                    //执行SQL语句
                    _IsSuccess = ExecuteNonQuery(SqlTexts, commandParameters, IsTransaction);
                }
            }
            catch (Exception ex)
            {

            }
            return _IsSuccess;
        }
        #endregion

        #endregion ExecuteNonQuery命令

        #region ExecuteDataset方法

        #region 参数：SQL语句
        /// <summary>
        /// 执行指定SQL语句,返回DataSet.
        /// </summary>
        /// <param name="SqlText">存储过程名称或SQL语句</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public DataSet ExecuteDataset(String SqlText, CommandType CmdType = CommandType.Text)
        {
            DataSet _DataSet = null;
            Guid _key = Guid.NewGuid();
            try
            {
                switch (connType)
                {
                    #region SqlClient
                    case ConnTypeEnum.SqlClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                            {
                                _DataSet = SqlServerHelper.ExecuteDataset(_SqlConnection, CmdType, SqlText);
                            }
                        }
                        else//如果使用连接池
                        {
                            SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _DataSet = SqlServerHelper.ExecuteDataset(_SqlConnection, CmdType, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OleDb
                    case ConnTypeEnum.OleDb:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                            {
                                _DataSet = AccessHelper.ExecuteDataset(_OleDbConnection, SqlText);
                            }
                        }
                        else//如果使用连接池
                        {
                            OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                            _DataSet = AccessHelper.ExecuteDataset(_OleDbConnection, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OracleClient
                    case ConnTypeEnum.OracleClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                            {
                                _DataSet = OracleHelper.ExecuteDataset(_OracleConnection, CmdType, SqlText);
                            }
                        }
                        else//如果使用连接池
                        {
                            OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                            _DataSet = OracleHelper.ExecuteDataset(_OracleConnection, CmdType, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region MySqlClient
                    case ConnTypeEnum.MySqlClient:
                        MySQLHelper _MySQLHelper = new MySQLHelper();
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                            {
                                _DataSet = _MySQLHelper.ExecuteDataSet(_MySqlConnection, CmdType, SqlText);
                            }
                        }
                        else//如果使用连接池
                        {
                            MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _DataSet = _MySQLHelper.ExecuteDataSet(_MySqlConnection, CmdType, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            return _DataSet;
        }
        /// <summary>
        /// 执行指定SQL语句,返回DataSet.
        /// </summary>
        /// <param name="SqlNum">存储过程名称或SQL语句的序号</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public DataSet ExecuteDatasetByNum(String SqlNum, CommandType CmdType = CommandType.Text)
        {
            DataSet _DataSet = null;
            try
            {
                //实例化获取SQL语句帮助类
                SQLCodeHelp _SQLCodeHelp = new SQLCodeHelp();
                //根据序号获取SQL语句
                string _SqlText = _SQLCodeHelp.GetSQLCode(SqlNum);
                //判断SQL语句是否为空
                if (!string.IsNullOrEmpty(_SqlText))
                {
                    //执行SQL语句
                    _DataSet = ExecuteDataset(_SqlText, CmdType);
                }
            }
            catch (Exception ex)
            {

            }
            return _DataSet;
        }
        #endregion

        #region 参数：SQL语句、执行命令所用参数的集合
        /// <summary>
        /// 执行指定SQL语句,返回DataSet.提供参数
        /// </summary>
        /// <param name="SqlText">存储过程名称或SQL语句</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public DataSet ExecuteDataset(String SqlText, CommandType CmdType = CommandType.Text, params object[] commandParameters)
        {
            DataSet _DataSet = null;
            Guid _key = Guid.NewGuid();
            try
            {
                switch (connType)
                {
                    #region SqlClient
                    case ConnTypeEnum.SqlClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                            {
                                _DataSet = SqlServerHelper.ExecuteDataset(_SqlConnection, CmdType, SqlText, (SqlParameter[])commandParameters);
                            }
                        }
                        else//如果使用连接池
                        {
                            SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _DataSet = SqlServerHelper.ExecuteDataset(_SqlConnection, CmdType, SqlText, (SqlParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OleDb
                    case ConnTypeEnum.OleDb:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                            {
                                _DataSet = AccessHelper.ExecuteDataset(_OleDbConnection, SqlText, (OleDbParameter[])commandParameters);
                            }
                        }
                        else//如果使用连接池
                        {
                            OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                            _DataSet = AccessHelper.ExecuteDataset(_OleDbConnection, SqlText, (OleDbParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OracleClient
                    case ConnTypeEnum.OracleClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                            {
                                _DataSet = OracleHelper.ExecuteDataset(_OracleConnection, CmdType, SqlText, (OracleParameter[])commandParameters);
                            }
                        }
                        else//如果使用连接池
                        {
                            OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                            _DataSet = OracleHelper.ExecuteDataset(_OracleConnection, CmdType, SqlText, (OracleParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region MySqlClient
                    case ConnTypeEnum.MySqlClient:
                        MySQLHelper _MySQLHelper = new MySQLHelper();
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                            {
                                _DataSet = _MySQLHelper.ExecuteDataSet(_MySqlConnection, CmdType, SqlText, (MySqlParameter[])commandParameters);
                            }
                        }
                        else//如果使用连接池
                        {
                            MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _DataSet = _MySQLHelper.ExecuteDataSet(_MySqlConnection, CmdType, SqlText, (MySqlParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            return _DataSet;
        }
        /// <summary>
        /// 执行指定SQL语句,返回DataSet.提供参数
        /// </summary>
        /// <param name="SqlNum">存储过程名称或SQL语句的序号</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public DataSet ExecuteDatasetByNum(String SqlNum, CommandType CmdType = CommandType.Text, params object[] commandParameters)
        {
            DataSet _DataSet = null;
            try
            {
                //实例化获取SQL语句帮助类
                SQLCodeHelp _SQLCodeHelp = new SQLCodeHelp();
                //根据序号获取SQL语句
                string _SqlText = _SQLCodeHelp.GetSQLCode(SqlNum);
                //判断SQL语句是否为空
                if (!string.IsNullOrEmpty(_SqlText))
                {
                    //执行SQL语句
                    _DataSet = ExecuteDataset(_SqlText, CmdType, commandParameters);
                }
            }
            catch (Exception ex)
            {

            }
            return _DataSet;
        }
        #endregion

        #endregion ExecuteDataset方法

        #region ExecuteReader 数据阅读器

        #region 参数：SQL语句
        /// <summary>
        /// 执行指定数据库连接对象的数据阅读器.
        /// </summary>
        /// <param name="SqlText">存储过程名称或SQL语句</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <returns>返回包含结果集的SqlDataReader</returns>
        public DbDataReader ExecuteReader(String SqlText, CommandType CmdType = CommandType.Text)
        {
            DbDataReader _DbDataReader = null;
            Guid _key = Guid.NewGuid();
            try
            {
                switch (connType)
                {
                    #region SqlClient
                    case ConnTypeEnum.SqlClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            //using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                            //{
                            SqlConnection _SqlConnection = new SqlConnection(connectionString);
                            _DbDataReader = SqlServerHelper.ExecuteReader(_SqlConnection, CmdType, SqlText);
                            //}
                        }
                        else//如果使用连接池
                        {
                            SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _DbDataReader = SqlServerHelper.ExecuteReader(_SqlConnection, CmdType, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OleDb
                    case ConnTypeEnum.OleDb:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            //using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                            //{
                            OleDbConnection _OleDbConnection = new OleDbConnection(connectionString);
                            _DbDataReader = AccessHelper.ExecuteReader(_OleDbConnection, SqlText);
                            //}
                        }
                        else//如果使用连接池
                        {
                            OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                            _DbDataReader = AccessHelper.ExecuteReader(_OleDbConnection, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OracleClient
                    case ConnTypeEnum.OracleClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            //using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                            //{
                            OracleConnection _OracleConnection = new OracleConnection(connectionString);
                            _DbDataReader = OracleHelper.ExecuteReader(_OracleConnection, CmdType, SqlText);
                            //}
                        }
                        else//如果使用连接池
                        {
                            OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                            _DbDataReader = OracleHelper.ExecuteReader(_OracleConnection, CmdType, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region MySqlClient
                    case ConnTypeEnum.MySqlClient:
                        MySQLHelper _MySQLHelper = new MySQLHelper();
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            //using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                            //{
                            MySqlConnection _MySqlConnection = new MySqlConnection(connectionString);
                            _DbDataReader = _MySQLHelper.ExecuteReader(_MySqlConnection, CmdType, SqlText);
                            //}
                        }
                        else//如果使用连接池
                        {
                            MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _DbDataReader = _MySQLHelper.ExecuteReader(_MySqlConnection, CmdType, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            return _DbDataReader;
        }
        /// <summary>
        /// 执行指定数据库连接对象的数据阅读器.
        /// </summary>
        /// <param name="SqlNum">存储过程名称或SQL语句的序号</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <returns>返回包含结果集的SqlDataReader</returns>
        public DbDataReader ExecuteReaderByNum(String SqlNum, CommandType CmdType = CommandType.Text)
        {
            DbDataReader _DbDataReader = null;
            try
            {
                //实例化获取SQL语句帮助类
                SQLCodeHelp _SQLCodeHelp = new SQLCodeHelp();
                //根据序号获取SQL语句
                string _SqlText = _SQLCodeHelp.GetSQLCode(SqlNum);
                //判断SQL语句是否为空
                if (!string.IsNullOrEmpty(_SqlText))
                {
                    //执行SQL语句
                    _DbDataReader = ExecuteReader(_SqlText, CmdType);
                }
            }
            catch (Exception ex)
            {

            }
            return _DbDataReader;
        }
        #endregion

        #region 参数：SQL语句、执行命令所用参数的集合
        /// <summary>
        /// 执行指定数据库连接对象的数据阅读器.提供参数
        /// </summary>
        /// <param name="SqlText">存储过程名称或SQL语句</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回包含结果集的SqlDataReader</returns>
        public DbDataReader ExecuteReader(String SqlText, CommandType CmdType = CommandType.Text, params object[] commandParameters)
        {
            DbDataReader _DbDataReader = null;
            Guid _key = Guid.NewGuid();
            try
            {
                switch (connType)
                {
                    #region SqlClient
                    case ConnTypeEnum.SqlClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            //using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                            //{
                            SqlConnection _SqlConnection = new SqlConnection(connectionString);
                            _DbDataReader = SqlServerHelper.ExecuteReader(_SqlConnection, CmdType, SqlText, (SqlParameter[])commandParameters);
                            //}
                        }
                        else//如果使用连接池
                        {
                            SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _DbDataReader = SqlServerHelper.ExecuteReader(_SqlConnection, CmdType, SqlText, (SqlParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OleDb
                    case ConnTypeEnum.OleDb:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            //using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                            //{
                            OleDbConnection _OleDbConnection = new OleDbConnection(connectionString);
                            _DbDataReader = AccessHelper.ExecuteReader(_OleDbConnection, SqlText, (OleDbParameter[])commandParameters);
                            //}
                        }
                        else//如果使用连接池
                        {
                            OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                            _DbDataReader = AccessHelper.ExecuteReader(_OleDbConnection, SqlText, (OleDbParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OracleClient
                    case ConnTypeEnum.OracleClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            //using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                            //{
                            OracleConnection _OracleConnection = new OracleConnection(connectionString);
                            _DbDataReader = OracleHelper.ExecuteReader(_OracleConnection, CmdType, SqlText, (OracleParameter[])commandParameters);
                            //}
                        }
                        else//如果使用连接池
                        {
                            OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                            _DbDataReader = OracleHelper.ExecuteReader(_OracleConnection, CmdType, SqlText, (OracleParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region MySqlClient
                    case ConnTypeEnum.MySqlClient:
                        MySQLHelper _MySQLHelper = new MySQLHelper();
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            //using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                            //{
                            MySqlConnection _MySqlConnection = new MySqlConnection(connectionString);
                            _DbDataReader = _MySQLHelper.ExecuteReader(_MySqlConnection, CmdType, SqlText, (MySqlParameter[])commandParameters);
                            //}
                        }
                        else//如果使用连接池
                        {
                            MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _DbDataReader = _MySQLHelper.ExecuteReader(_MySqlConnection, CmdType, SqlText, (MySqlParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            return _DbDataReader;
        }
        /// <summary>
        /// 执行指定数据库连接对象的数据阅读器.提供参数
        /// </summary>
        /// <param name="SqlNum">存储过程名称或SQL语句的序号</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回包含结果集的SqlDataReader</returns>
        public DbDataReader ExecuteReaderByNum(String SqlNum, CommandType CmdType = CommandType.Text, params object[] commandParameters)
        {
            DbDataReader _DbDataReader = null;
            try
            {
                //实例化获取SQL语句帮助类
                SQLCodeHelp _SQLCodeHelp = new SQLCodeHelp();
                //根据序号获取SQL语句
                string _SqlText = _SQLCodeHelp.GetSQLCode(SqlNum);
                //判断SQL语句是否为空
                if (!string.IsNullOrEmpty(_SqlText))
                {
                    //执行SQL语句
                    _DbDataReader = ExecuteReader(_SqlText, CmdType, commandParameters);
                }
            }
            catch (Exception ex)
            {

            }
            return _DbDataReader;
        }
        #endregion

        #endregion ExecuteReader 数据阅读器

        #region ExecuteScalar 返回结果集中的第一行第一列

        #region 参数：SQL语句
        /// <summary>
        /// 执行指定SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="SqlText">存储过程名称或SQL语句</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public object ExecuteScalar(String SqlText, CommandType CmdType = CommandType.Text)
        {
            object _obj = null;
            Guid _key = Guid.NewGuid();
            try
            {
                switch (connType)
                {
                    #region SqlClient
                    case ConnTypeEnum.SqlClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                            {
                                _obj = SqlServerHelper.ExecuteScalar(_SqlConnection, CmdType, SqlText);
                            }
                        }
                        else//如果使用连接池
                        {
                            SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _obj = SqlServerHelper.ExecuteScalar(_SqlConnection, CmdType, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OleDb
                    case ConnTypeEnum.OleDb:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                            {
                                _obj = AccessHelper.ExecuteScalar(_OleDbConnection, SqlText);
                            }
                        }
                        else//如果使用连接池
                        {
                            OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                            _obj = AccessHelper.ExecuteScalar(_OleDbConnection, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OracleClient
                    case ConnTypeEnum.OracleClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                            {
                                _obj = OracleHelper.ExecuteScalar(_OracleConnection, CmdType, SqlText);
                            }
                        }
                        else//如果使用连接池
                        {
                            OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                            _obj = OracleHelper.ExecuteScalar(_OracleConnection, CmdType, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region MySqlClient
                    case ConnTypeEnum.MySqlClient:
                        MySQLHelper _MySQLHelper = new MySQLHelper();
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                            {
                                _obj = _MySQLHelper.ExecuteScalar(_MySqlConnection, CmdType, SqlText);
                            }
                        }
                        else//如果使用连接池
                        {
                            MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _obj = _MySQLHelper.ExecuteScalar(_MySqlConnection, CmdType, SqlText);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            return _obj;
        }
        /// <summary>
        /// 执行指定SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="SqlNum">存储过程名称或SQL语句的序号</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public object ExecuteScalarByNum(String SqlNum, CommandType CmdType = CommandType.Text)
        {
            object _Object = null;
            try
            {
                //实例化获取SQL语句帮助类
                SQLCodeHelp _SQLCodeHelp = new SQLCodeHelp();
                //根据序号获取SQL语句
                string _SqlText = _SQLCodeHelp.GetSQLCode(SqlNum);
                //判断SQL语句是否为空
                if (!string.IsNullOrEmpty(_SqlText))
                {
                    //执行SQL语句
                    _Object = ExecuteScalar(_SqlText, CmdType);
                }
            }
            catch (Exception ex)
            {

            }
            return _Object;
        }
        #endregion

        #region 参数：SQL语句、执行命令所用参数的集合
        /// <summary>
        /// 执行指定SQL语句，返回结果集中的第一行第一列,提供参数
        /// </summary>
        /// <param name="SqlText">存储过程名称或SQL语句</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public object ExecuteScalar(String SqlText, CommandType CmdType = CommandType.Text, params object[] commandParameters)
        {
            object _obj = null;
            Guid _key = Guid.NewGuid();
            try
            {
                switch (connType)
                {
                    #region SqlClient
                    case ConnTypeEnum.SqlClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (SqlConnection _SqlConnection = new SqlConnection(connectionString))
                            {
                                _obj = SqlServerHelper.ExecuteScalar(_SqlConnection, CmdType, SqlText, (SqlParameter[])commandParameters);
                            }
                        }
                        else//如果使用连接池
                        {
                            SqlConnection _SqlConnection = (SqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _obj = SqlServerHelper.ExecuteScalar(_SqlConnection, CmdType, SqlText, (SqlParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OleDb
                    case ConnTypeEnum.OleDb:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OleDbConnection _OleDbConnection = new OleDbConnection(connectionString))
                            {
                                _obj = AccessHelper.ExecuteScalar(_OleDbConnection, SqlText, (OleDbParameter[])commandParameters);
                            }
                        }
                        else//如果使用连接池
                        {
                            OleDbConnection _OleDbConnection = (OleDbConnection)connectionPool.GetConnectionFormPool(_key);
                            _obj = AccessHelper.ExecuteScalar(_OleDbConnection, SqlText, (OleDbParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region OracleClient
                    case ConnTypeEnum.OracleClient:
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (OracleConnection _OracleConnection = new OracleConnection(connectionString))
                            {
                                _obj = OracleHelper.ExecuteScalar(_OracleConnection, CmdType, SqlText, (OracleParameter[])commandParameters);
                            }
                        }
                        else//如果使用连接池
                        {
                            OracleConnection _OracleConnection = (OracleConnection)connectionPool.GetConnectionFormPool(_key);
                            _obj = OracleHelper.ExecuteScalar(_OracleConnection, CmdType, SqlText, (OracleParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                    #region MySqlClient
                    case ConnTypeEnum.MySqlClient:
                        MySQLHelper _MySQLHelper = new MySQLHelper();
                        if (!isConnectionPool)//如果不使用连接池
                        {
                            using (MySqlConnection _MySqlConnection = new MySqlConnection(connectionString))
                            {
                                _obj = _MySQLHelper.ExecuteScalar(_MySqlConnection, CmdType, SqlText, (MySqlParameter[])commandParameters);
                            }
                        }
                        else//如果使用连接池
                        {
                            MySqlConnection _MySqlConnection = (MySqlConnection)connectionPool.GetConnectionFormPool(_key);
                            _obj = _MySQLHelper.ExecuteScalar(_MySqlConnection, CmdType, SqlText, (MySqlParameter[])commandParameters);
                            connectionPool.DisposeConnection(_key);
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            return _obj;
        }
        /// <summary>
        /// 执行指定SQL语句，返回结果集中的第一行第一列,提供参数
        /// </summary>
        /// <param name="SqlNum">存储过程名称或SQL语句的序号</param>
        /// <param name="CmdType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public object ExecuteScalarByNum(String SqlNum, CommandType CmdType = CommandType.Text, params object[] commandParameters)
        {
            object _Object = null;
            try
            {
                //实例化获取SQL语句帮助类
                SQLCodeHelp _SQLCodeHelp = new SQLCodeHelp();
                //根据序号获取SQL语句
                string _SqlText = _SQLCodeHelp.GetSQLCode(SqlNum);
                //判断SQL语句是否为空
                if (!string.IsNullOrEmpty(_SqlText))
                {
                    //执行SQL语句
                    _Object = ExecuteScalar(_SqlText, CmdType, commandParameters);
                }
            }
            catch (Exception ex)
            {

            }
            return _Object;
        }
        #endregion

        #endregion ExecuteScalar 返回结果集中的第一行第一列

        #region Microsoft实体模型
        /// <summary>
        /// 获取EntityConnection连接
        /// </summary>
        /// <param name="_key">申请key（此连接的唯一凭证，释放连接时用到）</param>
        /// <returns></returns>
        public EntityConnection GetEntityConnection(object _key = null)
        {
            EntityConnection entityCoon = null;
            try
            {
                if (!isConnectionPool)//如果不使用连接池
                {
                    entityCoon = new EntityConnection(connectionString);
                }
                else
                {
                    entityCoon = (EntityConnection)connectionPool.GetConnectionFormPool(_key);
                }
            }
            catch (Exception ex)
            {

            }
            return entityCoon;
        }
        /// <summary>
        /// 释放EntityConnection连接到连接池
        /// </summary>
        /// <param name="_key">申请key（此连接的唯一凭证，申请连接时的key）</param>
        public void DisposeEntityConnection(object _key)
        {
            try
            {
                connectionPool.DisposeConnection(_key);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
