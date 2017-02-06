using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Collections.Generic;

namespace TinyStack.Modules.iSqlHelper
{
    #region 连接池状态
    /// <summary>
    /// 连接池状态
    /// </summary>
    public enum PoolState
    {
        /// <summary>
        /// 刚刚创建的对象，表示该对象未被调用过StartSeivice方法。
        /// </summary>
        UnInitialize,
        /// <summary>
        /// 初始化中，该状态下服务正在按照参数初始化连接池。
        /// </summary>
        Initialize,
        /// <summary>
        /// 运行中
        /// </summary>
        Run,
        /// <summary>
        /// 停止状态
        /// </summary>
        Stop
    }
    #endregion

    #region 要申请连接的级别
    /// <summary>
    /// 要申请连接的级别
    /// </summary>
    public enum ConnLevel
    {
        /// <summary>
        /// 独占方式，分配全新的连接资源，并且该连接资源在本次使用释放回连接池之前不能在分配出去。如果连接池只能分配引用记数类型连接资源则该级别将产生一个异常，标志连接池资源耗尽
        /// </summary>
        ReadOnly,
        /// <summary>
        /// 优先级-高，分配全新的连接资源，不使用引用记数技术。注：此级别不保证在分配后该连接资源后，仍然保持独立占有资源，若想独立占有资源请使用ReadOnely
        /// </summary>
        High,
        /// <summary>
        /// 优先级-中，适当应用引用记数技术分配连接
        /// </summary>
        None,
        /// <summary>
        /// 优先级-底，尽可能使用引用记数技术分配连接
        /// </summary>
        Bottom
    }
    #endregion

    #region 连接类型
    /// <summary>
    /// 连接类型
    /// </summary>
    public enum ConnTypeEnum
    {
        /// <summary>
        /// ODBC 数据源
        /// </summary>
        Odbc,
        /// <summary>
        /// OLE DB 数据源,Access数据库
        /// </summary>
        OleDb,
        /// <summary>
        /// SqlServer 数据库连接
        /// </summary>
        SqlClient,
        /// <summary>
        /// MySqlServer 数据库连接
        /// </summary>
        MySqlClient,
        /// <summary>
        /// Oracle 数据库连接
        /// </summary>
        OracleClient,
        /// <summary>
        /// Miscrosoft实体模型连接
        /// </summary>
        MsModelClient,
        /// <summary>
        /// 默认（无分配）
        /// </summary>
        None
    }
    #endregion

    #region 连接池中的一个连接类型
    /// <summary>
    /// 连接池中的一个连接类型
    /// </summary>
    public class ConnStruct : IDisposable
    {
        #region 连接池中的连接
        /// <summary>
        /// 连接池中的连接
        /// </summary>
        /// <param name="dbc">数据库连接</param>
        /// <param name="cte">连接类型</param>
        public ConnStruct(DbConnection dbc, ConnTypeEnum cte)
        {
            createTime = DateTime.Now;
            connect = dbc;
            connType = cte;
        }
        /// <summary>
        /// 连接池中的连接
        /// </summary>
        /// <param name="dbc">数据库连接</param>
        /// <param name="cte">连接类型</param>
        /// <param name="dt">连接创建时间</param>
        public ConnStruct(DbConnection dbc, ConnTypeEnum cte, DateTime dt)
        {
            createTime = dt;
            connect = dbc;
            connType = cte;
        }
        #endregion

        #region 声明变量
        private bool enable = true;//是否失效
        private bool use = false;//是否正在被使用中
        private bool allot = true;//表示该连接是否可以被分配
        private DateTime createTime = DateTime.Now;//创建时间
        private int useDegree = 0;//被使用次数
        private int repeatNow = 0;//当前连接被重复引用多少
        private bool isRepeat = true;//连接是否可以被重复引用，当被分配出去的连接可能使用事务时，该属性被标识为true
        private ConnTypeEnum connType = ConnTypeEnum.None;//连接类型
        private DbConnection connect = null;//连接对象
        private object obj = null;//连接附带的信息
        #endregion

        #region 属性部分
        /// <summary>
        /// 表示该连接是否可以被分配
        /// </summary>
        public bool Allot
        {
            get { return allot; }
            set { allot = value; }
        }
        /// <summary>
        /// 是否失效；false表示失效，只读
        /// </summary>
        public bool Enable
        { get { return enable; } }
        /// <summary>
        /// 是否正在被使用中，只读
        /// </summary>
        public bool IsUse
        { get { return use; } }
        /// <summary>
        /// 创建时间，只读
        /// </summary>
        public DateTime CreateTime
        { get { return createTime; } }
        /// <summary>
        /// 被使用次数，只读
        /// </summary>
        public int UseDegree
        { get { return useDegree; } }
        /// <summary>
        /// 当前连接被重复引用多少，只读
        /// </summary>
        public int RepeatNow
        { get { return repeatNow; } }
        /// <summary>
        /// 得到数据库连接状态，只读
        /// </summary>
        public ConnectionState State
        { get { return connect.State; } }
        /// <summary>
        /// 得到该连接，只读
        /// </summary>
        public DbConnection Connection
        { get { return connect; } }
        /// <summary>
        /// 连接是否可以被重复引用
        /// </summary>
        public bool IsRepeat
        {
            get { return isRepeat; }
            set { isRepeat = value; }
        }
        /// <summary>
        /// 连接类型，只读
        /// </summary> 
        public ConnTypeEnum ConnType
        { get { return connType; } }
        /// <summary>
        /// 连接附带的信息
        /// </summary>
        public object Obj
        {
            get { return obj; }
            set { obj = value; }
        }
        #endregion

        #region 打开数据库连接
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            connect.Open();
        }
        #endregion

        #region 关闭数据库连接
        /// <summary>
        /// 关闭数据库连接 
        /// </summary>
        public void Close()
        {
            connect.Close();
        }
        #endregion

        #region 无条件将连接设置为失效
        /// <summary>
        /// 无条件将连接设置为失效
        /// </summary>
        public void SetConnectionLost()
        {
            enable = false; allot = false;
        }
        #endregion

        #region 被分配出去，线程安全的
        /// <summary>
        /// 被分配出去，线程安全的
        /// </summary>
        public void Repeat()
        {
            lock (this)
            {
                if (enable == false)//连接可用
                    throw new ResLostnExecption();//连接资源已经失效
                if (allot == false)//是否可以被分配
                    throw new AllotExecption();//连接资源不可以被分配
                if (use == true && isRepeat == false)
                    throw new AllotAndRepeatExecption();//连接资源已经被分配并且不允许重复引用
                repeatNow++;//引用记数+1
                useDegree++;//被使用次数+1
                use = true;//被使用
            }
        }
        #endregion

        #region 被释放回来，线程安全的
        /// <summary>
        /// 被释放回来，线程安全的
        /// </summary>
        public void Remove()
        {
            lock (this)
            {
                if (enable == false)//连接可用
                    throw new ResLostnExecption();//连接资源已经失效
                if (repeatNow == 0)
                    throw new RepeatIsZeroExecption();//引用记数已经为0
                repeatNow--;//引用记数-1
                if (repeatNow == 0)
                    use = false;//未使用
                else
                    use = true;//使用中
            }
        }
        #endregion

        #region 释放资源
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            enable = false;
            connect.Close();
            connect = null;
        }
        #endregion
    }
    #endregion

    #region PoolException
    /// <summary>
    /// 服务未启动
    /// </summary>
    public class PoolNotRunException : Exception
    {
        public PoolNotRunException() : base("服务未启动") { }
        public PoolNotRunException(string message) : base(message) { }
    }
    /// <summary>
    /// 服务已经运行或者未完全结束
    /// </summary>
    public class PoolNotStopException : Exception
    {
        public PoolNotStopException() : base("服务已经运行或者未完全结束") { }
        public PoolNotStopException(string message) : base(message) { }
    }
    /// <summary>
    /// 连接池资源未全部回收
    /// </summary>
    public class ResCallBackException : Exception
    {
        public ResCallBackException() : base("连接池资源未全部回收") { }
        public ResCallBackException(string message) : base(message) { }
    }
    /// <summary>
    /// 连接池已经饱和，不能提供连接
    /// </summary>
    public class PoolFullException : Exception
    {
        public PoolFullException() : base("连接池已经饱和，不能提供连接") { }
        public PoolFullException(string message) : base(message) { }
    }
    /// <summary>
    /// 服务状态错误
    /// </summary>
    public class StateException : Exception
    {
        public StateException() : base("服务状态错误") { }
        public StateException(string message) : base(message) { }
    }
    /// <summary>
    /// 一个key对象只能申请一个连接
    /// </summary>
    public class KeyExecption : Exception
    {
        public KeyExecption() : base("一个key对象只能申请一个连接") { }
        public KeyExecption(string message) : base(message) { }
    }
    /// <summary>
    /// 无法释放，不存在的key
    /// </summary>
    public class NotKeyExecption : Exception
    {
        public NotKeyExecption() : base("无法释放，不存在的key") { }
        public NotKeyExecption(string message) : base(message) { }
    }
    /// <summary>
    /// 当前连接池状态不可以对属性赋值
    /// </summary>
    public class SetValueExecption : Exception
    {
        public SetValueExecption() : base("当前连接池状态不可以对属性赋值") { }
        public SetValueExecption(string message) : base(message) { }
    }
    /// <summary>
    /// 参数范围错误
    /// </summary>
    public class ParameterBoundExecption : Exception
    {
        public ParameterBoundExecption() : base("参数范围错误") { }
        public ParameterBoundExecption(string message) : base(message) { }
    }
    /// <summary>
    /// 无效的ConnTypeEnum类型参数
    /// </summary>
    public class ConnTypeExecption : Exception
    {
        public ConnTypeExecption() : base("无效的ConnTypeEnum类型参数") { }
        public ConnTypeExecption(string message) : base(message) { }
    }
    /// <summary>
    /// 连接资源耗尽，或错误的访问时机。
    /// </summary>
    public class OccasionExecption : Exception
    {
        public OccasionExecption() : base("连接资源耗尽，或错误的访问时机。") { }
        public OccasionExecption(string message) : base(message) { }
    }
    /// <summary>
    /// 连接资源已经失效。
    /// </summary>
    public class ResLostnExecption : Exception
    {
        public ResLostnExecption() : base("连接资源已经失效。") { }
        public ResLostnExecption(string message) : base(message) { }
    }
    /// <summary>
    /// 连接资源不可以被分配。
    /// </summary>
    public class AllotExecption : Exception
    {
        public AllotExecption() : base("连接资源不可以被分配。") { }
        public AllotExecption(string message) : base(message) { }
    }
    /// <summary>
    /// 连接资源已经被分配并且不允许重复引用。
    /// </summary>
    public class AllotAndRepeatExecption : AllotExecption
    {
        public AllotAndRepeatExecption() : base("连接资源已经被分配并且不允许重复引用") { }
        public AllotAndRepeatExecption(string message) : base(message) { }
    }
    /// <summary>
    /// 引用记数已经为0。
    /// </summary>
    public class RepeatIsZeroExecption : Exception
    {
        public RepeatIsZeroExecption() : base("引用记数已经为0。") { }
        public RepeatIsZeroExecption(string message) : base(message) { }
    }
    #endregion

    #region 数据库连接池，默认数据库连接方案是ODBC
    /// <summary>
    /// 数据库连接池，默认数据库连接方案是ODBC
    /// </summary>
    public class ConnectionPool : IDisposable
    {
        #region 变量定义
        /// <summary>
        /// 连接池中存在的实际连接数(包含失效的连接)
        /// </summary>
        private int _realFormPool;
        /// <summary>
        /// 连接池中存在的实际连接数(有效的实际连接)
        /// </summary>
        private int _potentRealFormPool;
        /// <summary>
        /// 空闲的实际连接
        /// </summary>
        private int _spareRealFormPool;
        /// <summary>
        /// 已分配的实际连接
        /// </summary>
        private int _useRealFormPool;
        /// <summary>
        /// 连接池已经分配多少只读连接
        /// </summary>
        private int _readOnlyFormPool;
        /// <summary>
        /// 已经分配出去的连接数
        /// </summary>
        private int _useFormPool;
        /// <summary>
        /// 目前可以提供的连接数
        /// </summary>
        private int _spareFormPool;
        /// <summary>
        /// 最大连接数，最大可以创建的连接数目
        /// </summary>
        private int _maxConnection;
        /// <summary>
        /// 最小连接数
        /// </summary>
        private int _minConnection;
        /// <summary>
        /// 每次创建连接的连接数
        /// </summary>
        private int _seepConnection;
        /// <summary>
        /// 保留的实际空闲连接，以攻可能出现的ReadOnly使用，当空闲连接不足该数值时，连接池将创建seepConnection个连接
        /// </summary>
        private int _keepRealConnection;
        /// <summary>
        /// 每个连接生存期限 20分钟
        /// </summary>
        private int _exist = 20;

        private int _maxRepeatDegree = 5;
        //当连接池的连接被分配尽时，连接池会在已经分配出去的连接中，重复分配连接（引用记数）。来缓解连接池压力
        /// <summary>
        /// 服务启动时间
        /// </summary>
        private DateTime _startTime;
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string _connString = null;
        /// <summary>
        /// 连接池连接类型
        /// </summary>
        private ConnTypeEnum _connType;
        /// <summary>
        /// 连接池状态
        /// </summary>
        private PoolState _ps;
        //内部对象
        /// <summary>
        /// 实际连接
        /// </summary>
        private List<ConnStruct> al_All = new List<ConnStruct>();
        /// <summary>
        /// 正在使用的连接
        /// </summary>
        private Hashtable hs_UseConn = new Hashtable();
        /// <summary>
        /// 监视器记时器
        /// </summary>
        private System.Timers.Timer time;
        /// <summary>
        /// 创建线程
        /// </summary>
        private Thread threadCreate;
        private bool isThreadCheckRun = false;
        //private Mutex mUnique = new Mutex();
        #endregion

        #region 构造方法 与 初始化函数
        /// <summary>
        /// 初始化连接池
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public ConnectionPool(string connectionString)
        { InitConnectionPool(connectionString, ConnTypeEnum.SqlClient, 200, 30, 10, 5, 5); }
        /// <summary>
        /// 初始化连接池
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cte">数据库连接类型</param>
        public ConnectionPool(string connectionString, ConnTypeEnum cte)
        { InitConnectionPool(connectionString, cte, 200, 30, 10, 5, 5); }
        /// <summary>
        /// 初始化连接池
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cte">数据库连接类型</param>
        /// <param name="maxConnection">最大连接数，最大可以创建的连接数目</param>
        /// <param name="minConnection">最小连接数</param>
        public ConnectionPool(string connectionString, ConnTypeEnum cte, int maxConnection, int minConnection)
        { InitConnectionPool(connectionString, cte, maxConnection, minConnection, 10, 5, 5); }
        /// <summary>
        /// 初始化连接池
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cte">数据库连接类型</param>
        /// <param name="maxConnection">最大连接数，最大可以创建的连接数目</param>
        /// <param name="minConnection">最小连接数</param>
        /// <param name="seepConnection">每次创建连接的连接数</param>
        /// <param name="keepConnection">保留连接数，当空闲连接不足该数值时，连接池将创建seepConnection个连接</param>
        public ConnectionPool(string connectionString, ConnTypeEnum cte, int maxConnection, int minConnection, int seepConnection, int keepConnection)
        { InitConnectionPool(connectionString, cte, maxConnection, minConnection, seepConnection, keepConnection, 5); }
        /// <summary>
        /// 初始化连接池
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cte">数据库连接类型</param>
        /// <param name="maxConnection">最大连接数，最大可以创建的连接数目</param>
        /// <param name="minConnection">最小连接数</param>
        /// <param name="seepConnection">每次创建连接的连接数</param>
        /// <param name="keepConnection">保留连接数，当空闲连接不足该数值时，连接池将创建seepConnection个连接</param>
        /// <param name="keepRealConnection">当空闲的实际连接不足该值时创建连接，直到达到最大连接数</param>
        public ConnectionPool(string connectionString, ConnTypeEnum cte, int maxConnection, int minConnection, int seepConnection, int keepConnection, int keepRealConnection)
        { InitConnectionPool(connectionString, cte, maxConnection, minConnection, seepConnection, keepConnection, keepRealConnection); }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cte">数据库连接类型</param>
        /// <param name="maxConnection">最大连接数，最大可以创建的连接数目</param>
        /// <param name="minConnection">最小连接数</param>
        /// <param name="seepConnection">每次创建连接的连接数</param>
        /// <param name="keepConnection">保留连接数，当空闲连接不足该数值时，连接池将创建seepConnection个连接</param>
        /// <param name="keepRealConnection">当空闲的实际连接不足该值时创建连接，直到达到最大连接数</param>
        protected void InitConnectionPool(string connectionString, ConnTypeEnum cte, int maxConnection, int minConnection, int seepConnection, int keepConnection, int keepRealConnection)
        {
            if (cte == ConnTypeEnum.None)
                throw new ConnTypeExecption();//参数不能是None

            _ps = PoolState.UnInitialize;
            this._connString = connectionString;
            this._connType = cte;
            this._minConnection = minConnection;
            this._seepConnection = seepConnection;
            this._keepRealConnection = keepRealConnection;
            this._maxConnection = maxConnection;
            this.time = new System.Timers.Timer(500);
            this.time.Stop();
            this.time.Elapsed += new System.Timers.ElapsedEventHandler(time_Elapsed);
            this.threadCreate = new Thread(new ThreadStart(createThreadProcess));
            //启动服务
            StartServices();
        }

        #endregion

        #region 属性部分
        /// <summary>
        /// 连接池服务状态
        /// </summary>
        public PoolState State
        { get { return _ps; } }
        /// <summary>
        /// 连接池是否启动，改变该属性将相当于调用StartServices或StopServices方法，注：只有连接池处于Run，Stop状态情况下才可以对此属性赋值
        /// </summary>
        public bool Enable
        {
            get
            {
                if (_ps == PoolState.Run)
                    return true;
                else
                    return false;
            }
            set
            {
                if (_ps == PoolState.Run || _ps == PoolState.Stop)
                    if (value == true)
                        StartServices();
                    else
                        StopServices();
                else
                    throw new SetValueExecption();//只有连接池处于Run，Stop状态情况下才可以对此属性赋值
            }
        }
        /// <summary>
        /// 得到或设置连接类型
        /// </summary>
        public ConnTypeEnum ConnectionType
        {
            get { return _connType; }
            set
            {
                if (_ps == PoolState.Stop)
                    _connType = value;
                else
                    throw new SetValueExecption();//只有在Stop状态时才可操作
            }
        }
        /// <summary>
        /// 连接池使用的连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _connString; }
            set
            {
                if (_ps == PoolState.Stop)
                    _connString = value;
                else
                    throw new SetValueExecption();//只有在Stop状态时才可操作
            }
        }
        /// <summary>
        /// 得到服务器运行时间
        /// </summary>
        public DateTime RunTime
        {
            get
            {
                if (_ps == PoolState.Stop)
                    return new DateTime(DateTime.Now.Ticks - _startTime.Ticks);
                else
                    return new DateTime(0);
            }
        }
        /// <summary>
        /// 最小连接数
        /// </summary>
        public int MinConnection
        {
            get { return _minConnection; }
            set
            {
                if (value < _maxConnection && value > 0 && value >= _keepRealConnection)
                    _minConnection = value;
                else
                    throw new ParameterBoundExecption();//参数范围应该在 0~MaxConnection之间，并且应该大于KeepConnection
            }
        }
        /// <summary>
        /// 最大连接数，最大可以创建的连接数目
        /// </summary>
        public int MaxConnection
        {
            get { return _maxConnection; }
            set
            {
                if (value >= _minConnection && value > 0)
                    _maxConnection = value;
                else
                    throw new ParameterBoundExecption();//参数范围错误，参数应该大于minConnection
            }
        }
        /// <summary>
        /// 每次创建连接的连接数
        /// </summary>
        public int SeepConnection
        {
            get { return _seepConnection; }
            set
            {
                if (value > 0 && value < _maxConnection)
                    _seepConnection = value;
                else
                    throw new ParameterBoundExecption();//创建连接的步长应大于0，同时小于MaxConnection
            }
        }
        /// <summary>
        /// 保留的实际空闲连接，以攻可能出现的ReadOnly使用
        /// </summary>
        public int KeepRealConnection
        {
            get { return _keepRealConnection; }
            set
            {
                if (value >= 0 && value < _maxConnection)
                    _keepRealConnection = value;
                else
                    throw new ParameterBoundExecption();//保留连接数应大于等于0，同时小于MaxConnection
            }
        }
        /// <summary>
        /// 自动清理连接池的时间间隔
        /// </summary>
        public double Interval
        {
            get { return time.Interval; }
            set { time.Interval = value; }
        }
        /// <summary>
        /// 每个连接生存期限(单位分钟)，默认20分钟
        /// </summary>
        public int Exist
        {
            get { return _exist; }
            set
            {
                if (_ps == PoolState.Stop)
                    _exist = value;
                else
                    throw new PoolNotStopException();//只有在Stop状态下才可以操作
            }
        }
        /// <summary>
        /// 可以被重复使用次数（引用记数）当连接被重复分配该值所表示的次数时，该连接将不能被分配出去。
        /// 当连接池的连接被分配尽时，连接池会在已经分配出去的连接中，重复分配连接（引用记数）。来缓解连接池压力
        /// </summary>
        public int MaxRepeatDegree
        {
            get { return _maxRepeatDegree; }
            set
            {
                if (value >= 0)
                    _maxRepeatDegree = value;
                else
                    throw new ParameterBoundExecption();//重复引用次数应大于等于0
            }
        }
        /// <summary>
        /// 连接池最多可以提供多少个连接
        /// </summary>
        public int MaxConnectionFormPool
        { get { return _maxConnection * _maxRepeatDegree; } }
        /// <summary>
        /// 连接池中存在的实际连接数(有效的实际连接)
        /// </summary>
        public int PotentRealFormPool
        {
            get
            {
                if (_ps == PoolState.Run)
                    return _potentRealFormPool;
                else
                    throw new PoolNotRunException();//连接池处在非运行中
            }
        }
        /// <summary>
        /// 连接池中存在的实际连接数(包含失效的连接)
        /// </summary>
        public int RealFormPool
        {
            get
            {
                if (_ps == PoolState.Run)
                    return _realFormPool;
                else
                    throw new PoolNotRunException();//连接池处在非运行中
            }
        }
        /// <summary>
        /// 空闲的实际连接
        /// </summary>
        public int SpareRealFormPool
        {
            get
            {
                if (_ps == PoolState.Run)
                    return _spareRealFormPool;
                else
                    throw new PoolNotRunException();//连接池处在非运行中
            }
        }
        /// <summary>
        /// 已分配的实际连接
        /// </summary>
        public int UseRealFormPool
        {
            get
            {
                if (_ps == PoolState.Run)
                    return _useRealFormPool;
                else
                    throw new PoolNotRunException();//连接池处在非运行中
            }
        }
        /// <summary>
        /// 连接池已经分配多少只读连接
        /// </summary>
        public int ReadOnlyFormPool
        {
            get
            {
                if (_ps == PoolState.Run)
                    return _readOnlyFormPool;
                else
                    throw new PoolNotRunException();//连接池处在非运行中
            }
        }
        /// <summary>
        /// 已经分配的连接数
        /// </summary>
        public int UseFormPool
        {
            get
            {
                if (_ps == PoolState.Run)
                    return _useFormPool;
                else
                    throw new PoolNotRunException();//连接池处在非运行中
            }
        }
        /// <summary>
        /// 目前可以提供的连接数
        /// </summary>
        public int SpareFormPool
        {
            get
            {
                if (_ps == PoolState.Run)
                    return _spareFormPool;
                else
                    throw new PoolNotRunException();//连接池处在非运行中
            }
        }

        #endregion

        #region 启动服务 与 终止服务
        /// <summary>
        /// 启动服务，线程安全，同步调用
        /// </summary>
        public void StartServices()
        {
            StartServices(false);
        }
        /// <summary>
        /// 启动服务，线程安全
        /// </summary>
        /// <param name="ansy">是否异步调用True为是，异步调用指，用户调用该方法后，无须等待创建结束就可继续做其他操作</param>
        public void StartServices(bool ansy)
        {
            lock (this)
            {
                createThreadMode = 0;//工作模式0
                createThreadProcessRun = true;
                createThreadProcessTemp = _minConnection;
                if (_ps == PoolState.UnInitialize)
                    threadCreate.Start();
                else if (_ps == PoolState.Stop)
                    threadCreate.Interrupt();
                else
                    throw new PoolNotStopException();//服务已经运行或者未完全结束
                time.Start();
            }

            if (!ansy)
                while (threadCreate.ThreadState != ThreadState.WaitSleepJoin) { Thread.Sleep(50); }//等待可能存在的创建线程结束
        }
        /// <summary>
        /// 停止服务，线程安全
        /// </summary>
        public void StopServices()
        { StopServices(false); }
        /// <summary>
        /// 停止服务，线程安全
        /// <param name="needs">是否必须退出；如果指定为false与StartServices()功能相同，如果指定为true。将未收回的连接资源关闭，这将是危险的。认为可能你的程序正在使用此资源。</param>
        /// </summary>
        public void StopServices(bool needs)
        {
            lock (this)
            {
                if (_ps == PoolState.Run)
                {
                    lock (hs_UseConn)
                    {
                        if (needs == true)//必须退出
                            hs_UseConn.Clear();
                        else
                            if (hs_UseConn.Count != 0)
                                throw new ResCallBackException();//连接池资源未全部回收
                    }
                    time.Stop();
                    while (isThreadCheckRun) { Thread.Sleep(50); }//等待timer事件结束
                    createThreadProcessRun = false;
                    while (threadCreate.ThreadState != ThreadState.WaitSleepJoin) { Thread.Sleep(50); }//等待可能存在的创建线程结束
                    lock (al_All)
                    {
                        for (int i = 0; i < al_All.Count; i++)
                            al_All[i].Dispose();
                        al_All.Clear();
                    }
                    _ps = PoolState.Stop;
                }
                else
                    throw new PoolNotRunException();//服务未启动
            }
            UpdateAttribute();//更新属性
        }

        public void Dispose()
        {
            try
            {
                this.StopServices();
                threadCreate.Abort();
            }
            catch (Exception e) { }
        }

        #endregion

        #region 获得连接 与 释放连接
        /// <summary>
        /// 在连接池中申请一个连接，使用None级别，线程安全
        /// </summary>
        /// <param name="gui">发起者</param>
        /// <returns>返回申请到的连接</returns>
        public DbConnection GetConnectionFormPool(object key)
        { return GetConnectionFormPool(key, ConnLevel.None); }
        /// <summary>
        /// 在连接池中申请一个连接，线程安全
        /// </summary>
        /// <param name="key">申请者</param>
        /// <param name="cl">申请的连接级别</param>
        /// <returns>返回申请到的连接</returns>
        public DbConnection GetConnectionFormPool(object key, ConnLevel cl)
        {
            lock (this)
            {
                if (_ps != PoolState.Run)
                    throw new StateException();//服务状态错误
                if (hs_UseConn.Count == MaxConnectionFormPool)
                    throw new PoolFullException();//连接池已经饱和，不能提供连接
                if (hs_UseConn.ContainsKey(key))
                    throw new KeyExecption();//一个key对象只能申请一个连接

                if (cl == ConnLevel.ReadOnly)
                    return GetConnectionFormPool_ReadOnly(key);//ReadOnly级别
                else if (cl == ConnLevel.High)
                    return GetConnectionFormPool_High(key);//High级别
                else if (cl == ConnLevel.None)
                    return GetConnectionFormPool_None(key);//None级别
                else
                    return GetConnectionFormPool_Bottom(key);//Bottom级别
            }
        }
        /// <summary>
        /// 申请一个连接资源，只读方式，线程安全
        /// </summary>
        /// <param name="key">申请者</param>
        /// <returns>申请到的连接对象</returns>
        protected DbConnection GetConnectionFormPool_ReadOnly(object key)
        {
            ConnStruct cs = null;
            for (int i = 0; i < al_All.Count; i++)
            {
                cs = al_All[i];
                if (cs.Enable == false || cs.Allot == false || cs.UseDegree == _maxRepeatDegree || cs.IsUse == true)
                    continue;
                return GetConnectionFormPool_Return(key, cs, ConnLevel.ReadOnly); //返回得到的连接
            }
            return GetConnectionFormPool_Return(key, null, ConnLevel.ReadOnly);
        }
        /// <summary>
        /// 申请一个连接资源，优先级-高，线程安全
        /// </summary>
        /// <param name="key">申请者</param>
        /// <returns>申请到的连接对象</returns>
        protected DbConnection GetConnectionFormPool_High(object key)
        {
            ConnStruct cs = null;
            ConnStruct csTemp = null;

            for (int i = 0; i < al_All.Count; i++)
            {
                csTemp = al_All[i];
                if (csTemp.Enable == false || csTemp.Allot == false || csTemp.UseDegree == _maxRepeatDegree)//不可以分配跳出本次循环。
                {
                    csTemp = null;
                    continue;
                }
                if (csTemp.UseDegree == 0)//得到最合适的
                {
                    cs = csTemp;
                    break;
                }
                else//不是最合适的放置到最佳选择中
                {
                    if (cs != null)
                    {
                        if (csTemp.UseDegree < cs.UseDegree)
                            //与上一个最佳选择选出一个最佳的放置到cs中
                            cs = csTemp;
                    }
                    else
                        cs = csTemp;
                }
            }
            return GetConnectionFormPool_Return(key, cs, ConnLevel.High);//返回最合适的连接
        }
        /// <summary>
        /// 申请一个连接资源，优先级-中，线程安全
        /// </summary>
        /// <param name="key">申请者</param>
        /// <returns>申请到的连接对象</returns>
        protected DbConnection GetConnectionFormPool_None(object key)
        {
            List<ConnStruct> al = new List<ConnStruct>();
            ConnStruct cs = null;
            for (int i = 0; i < al_All.Count; i++)
            {
                cs = al_All[i];
                if (cs.Enable == false || cs.Allot == false || cs.UseDegree == _maxRepeatDegree)//不可以分配跳出本次循环。
                    continue;

                if (cs.Allot == true)
                    al.Add(cs);
            }
            if (al.Count == 0)
                return GetConnectionFormPool_Return(key, null, ConnLevel.None);//发出异常
            else
                return GetConnectionFormPool_Return(key, (al[al.Count / 2]), ConnLevel.None);//返回连接
        }
        /// <summary>
        /// 申请一个连接资源，优先级-低，线程安全
        /// </summary>
        /// <param name="key">申请者</param>
        /// <returns>申请到的连接对象</returns>
        protected DbConnection GetConnectionFormPool_Bottom(object key)
        {
            ConnStruct cs = null;
            ConnStruct csTemp = null;

            for (int i = 0; i < al_All.Count; i++)
            {
                csTemp = al_All[i];
                if (csTemp.Enable == false || csTemp.Allot == false || csTemp.UseDegree == _maxRepeatDegree)//不可以分配跳出本次循环。
                {
                    csTemp = null;
                    continue;
                }
                else//不是最合适的放置到最佳选择中
                {
                    if (cs != null)
                    {
                        if (csTemp.UseDegree > cs.UseDegree)
                            //与上一个最佳选择选出一个最佳的放置到cs中
                            cs = csTemp;
                    }
                    else
                        cs = csTemp;
                }
            }
            return GetConnectionFormPool_Return(key, cs, ConnLevel.Bottom);//返回最合适的连接
        }
        /// <summary>
        /// 返回DbConnection对象，同时做获得连接时的必要操作
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="cs">ConnStruct对象</param>
        /// <param name="cl">级别</param>
        /// <param name="readOnly">是否为只读属性</param>
        /// <returns></returns>
        private DbConnection GetConnectionFormPool_Return(object key, ConnStruct cs, ConnLevel cl)
        {
            try
            {
                if (cs == null)
                    throw new Exception();
                cs.Repeat();
                hs_UseConn.Add(key, cs);
                if (cl == ConnLevel.ReadOnly)
                {
                    cs.Allot = false;
                    cs.IsRepeat = false;
                }
            }
            catch (Exception e)
            {
                throw new OccasionExecption();//连接资源耗尽，或错误的访问时机。
            }
            finally
            {
                UpdateAttribute();//更新属性
            }
            return cs.Connection;
        }
        /// <summary>
        /// 释放申请的数据库连接对象，线程安全
        /// <param name="key">key表示数据库连接申请者</param>
        /// </summary>
        public void DisposeConnection(object key)
        {
            lock (hs_UseConn)
            {
                ConnStruct cs = null;
                if (_ps == PoolState.Run)
                {
                    if (!hs_UseConn.ContainsKey(key))
                        throw new NotKeyExecption();//无法释放，不存在的key
                    cs = (ConnStruct)hs_UseConn[key];
                    cs.IsRepeat = true;
                    if (cs.Allot == false)
                        if (cs.Enable == true)
                            cs.Allot = true;
                    cs.Remove();
                    hs_UseConn.Remove(key);
                }
                else
                    throw new PoolNotRunException();//服务未启动
            }
            UpdateAttribute();//更新属性
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 创建线程工作模式
        /// </summary>
        private int createThreadMode = 0;
        /// <summary>
        /// 需要创建的连接数
        /// </summary>
        private int createThreadProcessTemp = 0;
        /// <summary>
        /// 是否决定创建线程将继续工作，如果不继续工作则线程会将自己处于阻止状态
        /// </summary>
        private bool createThreadProcessRun = false;
        /// <summary>
        /// 创建线程
        /// </summary>
        private void createThreadProcess()
        {
            bool join = false;
            int createThreadProcessTemp_inside = createThreadProcessTemp;
            _ps = PoolState.Initialize;
            while (true)
            {
                join = false;
                _ps = PoolState.Run;
                if (createThreadProcessRun == false)
                {
                    //遇到终止命令
                    try
                    {
                        threadCreate.Join();
                    }
                    catch (Exception e)
                    {
                    }
                }
                else
                {
                    if (createThreadMode == 0)
                    {
                        //------------------------begin mode  创建模式
                        lock (al_All)
                        {
                            if (al_All.Count < createThreadProcessTemp_inside)
                                al_All.Add(CreateConnection(_connString, _connType));
                            else
                                join = true;
                        }
                        //------------------------end mode
                    }
                    else if (createThreadMode == 1)
                    {
                        //------------------------begin mode  增加模式
                        lock (al_All)
                        {
                            if (createThreadProcessTemp_inside != 0)
                            {
                                createThreadProcessTemp_inside--;
                                al_All.Add(CreateConnection(_connString, _connType));
                            }
                            else
                                join = true;
                        }
                        //------------------------end mode
                    }
                    else
                        join = true;
                    //-------------------------------------------------------------------------
                    if (join == true)
                    {
                        UpdateAttribute();//更新属性
                        try
                        {
                            createThreadProcessTemp = 0;
                            threadCreate.Join();
                        }
                        catch (Exception e)
                        { createThreadProcessTemp_inside = createThreadProcessTemp; }//得到传入的变量
                    }
                }
            }
        }
        /// <summary>
        /// 检测事件
        /// </summary>
        private void time_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ConnStruct cs = null;
            time.Stop();//关闭自己
            isThreadCheckRun = true;
            //如果正在执行创建连接则退出
            if (threadCreate.ThreadState != ThreadState.WaitSleepJoin)
                return;
            //------------------------------------------------------
            lock (al_All)
            {
                int n = 0;
                for (int i = 0; i < al_All.Count; i++)
                {
                    cs = al_All[i];
                    TestConnStruct(cs);//测试
                    if (cs.Enable == false && cs.RepeatNow == 0)//没有引用的失效连接
                    {
                        cs.Close();//关闭它
                        al_All.Remove(cs);//删除
                    }
                }
            }
            //------------------------------------------------------
            UpdateAttribute();//更新属性

            if (_spareRealFormPool < _keepRealConnection)//保留空闲实际连接数不足
                createThreadProcessTemp = GetNumOf(_realFormPool, _seepConnection, _maxConnection);
            else
                createThreadProcessTemp = 0;
            //if (createThreadProcessTemp != 0)
            //    Console.WriteLine("创建" + createThreadProcessTemp);

            if (createThreadProcessTemp != 0)
            {
                //启动创建线程，工作模式1
                createThreadMode = 1;
                threadCreate.Interrupt();
            }

            isThreadCheckRun = false;
            time.Start();//打开自己
        }
        /// <summary>
        /// 得到当前要增加的量
        /// </summary>
        private int GetNumOf(int nowNum, int seepNum, int maxNum)
        {
            if (maxNum >= nowNum + seepNum)
                return seepNum;
            else
                return maxNum - nowNum;
        }
        /// <summary>
        /// 用指定类型创建连接
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="cte">连接类型</param>
        /// <param name="dt">连接超时时间</param>
        /// <returns>返回创建的连接</returns>
        private ConnStruct CreateConnection(string conn, ConnTypeEnum cte)
        {
            DbConnection db = null;
            if (cte == ConnTypeEnum.Odbc)
                db = new System.Data.Odbc.OdbcConnection(conn);//ODBC数据源连接
            else if (cte == ConnTypeEnum.OleDb)
                db = new System.Data.OleDb.OleDbConnection(conn);//OLE DB数据连接,Access数据库
            else if (cte == ConnTypeEnum.SqlClient)
                db = new System.Data.SqlClient.SqlConnection(conn);//SqlServer数据库连接
            else if (cte == ConnTypeEnum.MySqlClient)
                db = new MySql.Data.MySqlClient.MySqlConnection(conn);//MySqlServer数据库连接
            else if (cte == ConnTypeEnum.OracleClient)
                db = new System.Data.OracleClient.OracleConnection(conn);//Oracle数据库连接
            else if (cte == ConnTypeEnum.MsModelClient)
                db = new System.Data.EntityClient.EntityConnection(conn);//Microsoft数据库连接
            ConnStruct cs = new ConnStruct(db, cte, DateTime.Now);
            cs.Open();
            return cs;
        }
        /// <summary>
        /// 测试ConnStruct是否过期
        /// </summary>
        /// <param name="cs">被测试的ConnStruct</param>
        private void TestConnStruct(ConnStruct cs)
        {
            //此次被分配出去的连接是否在此次之后失效
            if (cs.UseDegree == _maxRepeatDegree)
                cs.SetConnectionLost();//超过使用次数
            if (cs.CreateTime.AddMinutes(_exist).Ticks <= DateTime.Now.Ticks)
                cs.SetConnectionLost();//连接超时
            if (cs.Connection.State == ConnectionState.Closed)
                cs.SetConnectionLost();//连接被关闭
        }
        /// <summary>
        /// 更新属性
        /// </summary>
        private void UpdateAttribute()
        {
            int temp_readOnlyFormPool = 0;//连接池已经分配多少只读连接
            int temp_potentRealFormPool = 0;//连接池中存在的实际连接数(有效的实际连接)
            int temp_spareRealFormPool = 0;//空闲的实际连接
            int temp_useRealFormPool = 0;//已分配的实际连接
            int temp_spareFormPool = MaxConnectionFormPool;//目前可以提供的连接数
            //---------------------------------
            lock (hs_UseConn)
            {
                _useFormPool = hs_UseConn.Count;
            }
            //---------------------------------
            ConnStruct cs = null;
            int n = 0;
            lock (al_All)
            {
                _realFormPool = al_All.Count;
                for (int i = 0; i < al_All.Count; i++)
                {
                    cs = al_All[i];
                    //只读
                    if (cs.Allot == false && cs.IsUse == true && cs.IsRepeat == false)
                        temp_readOnlyFormPool++;
                    //有效的实际连接
                    if (cs.Enable == true)
                        temp_potentRealFormPool++;
                    //空闲的实际连接
                    if (cs.Enable == true && cs.IsUse == false)
                        temp_spareRealFormPool++;
                    //已分配的实际连接
                    if (cs.IsUse == true)
                        temp_useRealFormPool++;
                    //目前可以提供的连接数
                    if (cs.Allot == true)
                        temp_spareFormPool = temp_spareFormPool - cs.RepeatNow;
                    else
                        temp_spareFormPool = temp_spareFormPool - _maxRepeatDegree;
                }
            }
            _readOnlyFormPool = temp_readOnlyFormPool;
            _potentRealFormPool = temp_potentRealFormPool;
            _spareRealFormPool = temp_spareRealFormPool;
            _useRealFormPool = temp_useRealFormPool;
            _spareFormPool = temp_spareFormPool;
        }
        #endregion
    }
    #endregion
}
