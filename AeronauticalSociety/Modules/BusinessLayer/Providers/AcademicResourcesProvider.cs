using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AeronauticalSociety.Model;
using AeronauticalSociety.Log;
using MySql.Data.MySqlClient;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    #region 获取期刊列表检索条件
    /// <summary>
    /// 获取期刊列表检索条件
    /// </summary>
    public class GetPeriodicalListParam : SearchParamBase
    {
        #region 学术资源分类ID
        /// <summary>
        /// 学术资源分类ID 1：期刊；2：出版物； 3:论文；
        /// </summary>
        public int sourceTypeID { get; set; }
        #endregion

        #region 父级ID
        /// <summary>
        /// 父级ID
        /// </summary>
        public int parenId { get; set; }
        #endregion

        #region 排序方式
        /// <summary>
        /// 排序方式
        /// </summary>
        private string _orderType = "asc";
        /// <summary>
        /// 排序方式 正序：asc ；倒序：desc
        /// </summary>
        public string orderType { get { return _orderType; } set { _orderType = value; } }
        #endregion

        #region 年度
        /// <summary>
        /// 年度
        /// </summary>
        public string year { get; set; }
        #endregion

        #region 期数
        /// <summary>
        /// 期数
        /// </summary>
        public string periodNumber { get; set; }
        #endregion
    }
    #endregion

    #region 获取目录列表检索条件
    /// <summary>
    /// 获取目录列表检索条件
    /// </summary>
    public class GetAcademicResourcesMenuParam : SearchParamBase
    {
        #region 排序方式
        /// <summary>
        /// 排序方式
        /// </summary>
        private string _orderType = "asc";
        /// <summary>
        /// 排序方式 正序：asc ；倒序：desc
        /// </summary>
        public string orderType { get { return _orderType; } set { _orderType = value; } }
        #endregion
    }
    #endregion

    /// <summary>
    /// 学术资源
    /// </summary>
    public class AcademicResourcesProvider
    {
        #region 获取学术资源列表 应用
        /// <summary>
        /// 获取学术资源列表应用
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public BaseResopne<List<AcademicResources>> GetAcademicResourcesListForApp(GetPeriodicalListParam Param)
        {
            return GetAcademicResourcesList(Param, false);
        }
        #endregion

        #region 获取学术资源列表
        /// <summary>
        /// 获取学术资源列表
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="isPublish"></param>
        /// <returns></returns>
        private BaseResopne<List<AcademicResources>> GetAcademicResourcesList(GetPeriodicalListParam Param, bool isPublish)
        {
            BaseResopne<List<AcademicResources>> result = new BaseResopne<List<AcademicResources>>();
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlCount = new StringBuilder();
                sql.AppendLine(@"select dac.id,dac.`name`,dac.parentID,dac.sourceTypeID,dat.typeName as sourceTypeName
                            ,dac.createTime,u.userName as createUserName,dac.createUserID,dac.isMenu
                            ,dac.isChild,dac.author,`year`,periodNumber
                            from de2_aresource  dac
                            LEFT JOIN de2_user u on  u.id=dac.createUserID
                            LEFT JOIN de2_aresource_type dat on dat.id=dac.sourceTypeID where 1=1");
                sql.AppendLine("select count(id) from de2_aresource  where 1=1");
                //检索条件
                if (Param.sourceTypeID != 0)
                {
                    sql.AppendLine("sourceTypeID=@sourceTypeID");
                    sqlCount.AppendLine("sourceTypeID=@sourceTypeID");
                }
                //年度
                if (!string.IsNullOrEmpty(Param.year))
                {
                    sql.AppendLine("`year`=@`year`");
                    sqlCount.AppendLine("`year`=@`year`");
                }
                //期数
                if (!string.IsNullOrEmpty(Param.periodNumber))
                {
                    sql.AppendLine("periodNumber=@periodNumber");
                    sqlCount.AppendLine("periodNumber=@periodNumber");
                }
                //父级ID
                sql.AppendLine("  parentID=@parentID");
                sqlCount.AppendLine("  parentID=@parentID");
                //是否发布
                sql.AppendLine("  isPublish=@isPublish");
                sqlCount.AppendLine("  isPublish=@isPublish");

                //排序
                sql.AppendLine(" order by dam.createTime @orderByType");
                sql.AppendLine(" limit @start，@pageSize ");
                //执行sql
                MySqlClient _client = new MySqlClient();
                result.Total = Convert.ToInt32(_client.ExecuteScalar(sqlCount.ToString()
                    , new MySqlParameter("@sourceTypeID", Param.sourceTypeID)
                    , new MySqlParameter("@parentID", Param.parenId)
                    , new MySqlParameter("@`year`", Param.year)
                    , new MySqlParameter("@periodNumber", Param.periodNumber)
                    , new MySqlParameter("@isPublish", isPublish)));
                result.Data = _client.ExecuteQuery<AcademicResources>(sql.ToString()
                   , new MySqlParameter("@sourceTypeID", Param.sourceTypeID)
                   , new MySqlParameter("@orderByType", Param.orderType)
                   , new MySqlParameter("@parentID", Param.parenId)
                   , new MySqlParameter("@isPublish", isPublish)
                   , new MySqlParameter("@`year`", Param.year)
                   , new MySqlParameter("@periodNumber", Param.periodNumber)
                   , new MySqlParameter("@start", Param.StartRow)
                   , new MySqlParameter("@pageSize", Param.PageSize));
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术资源列表", "GetAcademicResourcesList", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 获取目录
        /// <summary>
        /// 获取目录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public BaseResopne<List<AcademicResources_Menu>> GetAcademicResourcesMenu(GetAcademicResourcesMenuParam Param)
        {
            BaseResopne<List<AcademicResources_Menu>> result = new BaseResopne<List<AcademicResources_Menu>>();
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlCount = new StringBuilder();
                sql.AppendLine(@"select dam.id,dam.title,dam.parentID,dam.createTime,dam.createUserID
                ,u.userName as createUserName,dam.sourceTypeID,dat.typeName as sourceTypeName,dam.author,lastModifyTime,lastModifyUsrID
                from de2_aresource_menu dam
                LEFT JOIN de2_user u on  u.id=dam.createUserID
                LEFT JOIN de2_aresource_type dat on dat.id=dam.sourceTypeID");
                sqlCount.AppendLine(@"select count(id)  from de2_aresource_menu ");
                //排序
                sql.AppendLine(" order by dam.createTime @orderByType");
                sql.AppendLine(" limit @start，@pageSize ");
                //执行
                MySqlClient _Client = new MySqlClient();
                result.Total = Convert.ToInt32(_Client.ExecuteScalar(sqlCount.ToString()));
                result.Data = _Client.ExecuteQuery<AcademicResources_Menu>(sqlCount.ToString()
                   , new MySqlParameter("@orderByType", Param.orderType)
                   , new MySqlParameter("@start", Param.StartRow)
                   , new MySqlParameter("@pageSize", Param.PageSize));
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取目录", "GetAcademicResourcesMenu", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 获取学术资源详细
        /// <summary>
        /// 获取学术资源详细
        /// </summary>
        /// <param name="parentID">父级ID</param>
        /// <returns></returns>
        public AcademicResources_File GetAcademicResourcesFile(int parentID)
        {
            AcademicResources_File result = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"select daf.id,title ,createTime ,createUserID ,u.userName as createUserName
                ,targetFileUrl,sourceTypeID,dat.typeName as sourceTypeName,parentID   
                from de2_aresource_file daf
                LEFT JOIN de2_user u on  u.id=daf.createUserID
                LEFT JOIN de2_aresource_type dat on dat.id=daf.sourceTypeID");
                //检索条件
                sql.AppendLine("where parentID=@parentID");
                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<AcademicResources_File>(sql.ToString(), new MySqlParameter("@parentID", parentID)).FirstOrDefault(); ;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取目录", "GetAcademicResourcesMenu", ex);
            }
            return result;
        }
        #endregion

        #region 获取学术资源列表 管理端
        /// <summary>
        /// 获取学术资源列表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public BaseResopne<List<AcademicResources>> GetAcademicResourcesListForManager(GetPeriodicalListParam Param)
        {
            return GetAcademicResourcesList(Param, true);
        }
        #endregion

        #region 保存学术资源信息
        /// <summary>
        /// 保存学术资源信息
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public BaseResopne<Int32> AddAcademicResources(AcademicResources Data)
        {
            BaseResopne<Int32> result = new BaseResopne<Int32>();
            try
            {
                //获取当前登录人
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Error = "获取当前用户信息失败";
                    return result;
                }
                Data.createUserID = user.id;
                Data.createTime = DateTime.Now;
                Data.lastModifyTime = DateTime.Now;
                Data.lastModifyUsrID = user.id;
                //生成sql
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"insert into de2_aresource (`name`,parentID,sourceTypeID,createTime,createUserID,isChild,author,isPublish,`year`,periodNumber,lastModifyTime,lastModifyUsrID,isMenu) values(@`name`,@parentID,@sourceTypeID,@createTime,@createUserID,@isChild,@author,@isPublish,@`year`,@periodNumber,@lastModifyTime,@lastModifyUsrID,@isMenu)");
                //执行sql语句
                MySqlClient _Client = new MySqlClient();
                var ResourceID = _Client.Inster(sql.ToString()
                       , new MySqlParameter("@`name`", Data.name)
                       , new MySqlParameter("@parentID", Data.parentID)
                       , new MySqlParameter("@sourceTypeID", Data.sourceTypeID)
                       , new MySqlParameter("@createTime", Data.createTime)
                       , new MySqlParameter("@createUserID", Data.createUserID)
                       , new MySqlParameter("@isChild", Data.isChild)
                       , new MySqlParameter("@author", Data.author)
                       , new MySqlParameter("@isPublish", Data.isPublish)
                       , new MySqlParameter("@`year`", Data.year)
                       , new MySqlParameter("@periodNumber", Data.periodNumber)
                       , new MySqlParameter("@lastModifyTime", Data.lastModifyTime)
                       , new MySqlParameter("@lastModifyUsrID", Data.lastModifyUsrID)
                       , new MySqlParameter("@isMenu", Data.isMenu));
                result.Data = Convert.ToInt32(ResourceID);
                //判断文件路径是否为空
                if (!string.IsNullOrEmpty(Data.resourceFile))
                {
                    AcademicResources_File _AcademicResources_File = new AcademicResources_File();
                    _AcademicResources_File.lastModifyTime = DateTime.Now;
                    _AcademicResources_File.lastModifyUsrID = user.id;
                    _AcademicResources_File.createTime = DateTime.Now;
                    _AcademicResources_File.createUserID = user.id;
                    _AcademicResources_File.parentID = Convert.ToInt32(ResourceID);
                    _AcademicResources_File.sourceTypeID = Data.sourceTypeID;
                    _AcademicResources_File.targetFileUrl = Data.resourceFile;
                    _AcademicResources_File.title = Data.name;
                    AddResourcesFile(_AcademicResources_File);
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "保存学术资源信息", "AddAcademicResources", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 修改学术资源信息
        /// <summary>
        /// 修改学术资源信息
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public BaseResopne<string> UpdateAcademicResources(AcademicResources Data)
        {
            BaseResopne<string> result = new BaseResopne<string>();
            try
            {
                //获取当前登录人
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Error = "获取当前用户信息失败";
                    return result;
                }
                Data.lastModifyTime = DateTime.Now;
                Data.lastModifyUsrID = user.id;
                //生成sql
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"update  de2_aresource set `name`=@`name`,parentID=@parentID,sourceTypeID=@sourceTypeID,isChild=@,author=@,isPublish=@isPublish,`year`=@`year`,periodNumber=@periodNumber,lastModifyTime=@lastModifyTime,lastModifyUsrID=@lastModifyUsrID,isMenu=@isMenu where id=@id");
                //执行sql语句
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql.ToString()
                    , new MySqlParameter("@`name`", Data.name)
                    , new MySqlParameter("@parentID", Data.parentID)
                    , new MySqlParameter("@sourceTypeID", Data.sourceTypeID)
                    , new MySqlParameter("@isChild", Data.isChild)
                    , new MySqlParameter("@author", Data.author)
                    , new MySqlParameter("@isPublish", Data.isPublish)
                    , new MySqlParameter("@`year`", Data.year)
                    , new MySqlParameter("@periodNumber", Data.periodNumber)
                    , new MySqlParameter("@lastModifyTime", Data.lastModifyTime)
                    , new MySqlParameter("@lastModifyUsrID", Data.lastModifyUsrID)
                    , new MySqlParameter("@isMenu", Data.isMenu)
                    , new MySqlParameter("@id", Data.id));
                //判断文件路径是否为空
                if (string.IsNullOrEmpty(Data.resourceFile))
                {
                    AcademicResources_File _AcademicResources_File = new AcademicResources_File();
                    _AcademicResources_File.lastModifyTime = DateTime.Now;
                    _AcademicResources_File.lastModifyUsrID = user.id;
                    _AcademicResources_File.parentID = Data.id;
                    _AcademicResources_File.sourceTypeID = Data.sourceTypeID;
                    _AcademicResources_File.targetFileUrl = Data.resourceFile;
                    _AcademicResources_File.title = Data.name;
                    UpdateResourcesFile(_AcademicResources_File);
                }
                else
                {
                    DelResourcesFileByParent(Data.id);
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改学术资源信息", "UpdateAcademicResources", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 修改学术资源发布状态
        /// <summary>
        /// 修改学术资源发布状态
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public BaseResopne<string> UpdatePublishStatue(AcademicResources Data)
        {
            BaseResopne<string> result = new BaseResopne<string>();
            try
            {
                //获取当前登录人
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Error = "获取当前用户信息失败";
                    return result;
                }
                Data.lastModifyTime = DateTime.Now;
                Data.lastModifyUsrID = user.id;

                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"update  de2_aresource set isPublish=@isPublish,lastModifyTime=@lastModifyTime,lastModifyUsrID=@lastModifyUsrID where id=@id");
                //执行sql语句
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql.ToString()
                    , new MySqlParameter("@isPublish", Data.isPublish)
                    , new MySqlParameter("@lastModifyTime", Data.lastModifyTime)
                    , new MySqlParameter("@lastModifyUsrID", Data.lastModifyUsrID)
                    , new MySqlParameter("@id", Data.id));
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改学术资源发布状态", "UpdatePublishStatue", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 获取学术资源详情
        /// <summary>
        /// 获取学术资源详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private AcademicResources GetAcademicResources(int id)
        {
            AcademicResources result = new AcademicResources();
            try
            {
                string sql = "select * from de2_aresource where id=@id ";
                //执行sql
                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<AcademicResources>(sql, new MySqlParameter("@id", id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术资源详情", "GetAcademicResources", ex);

            }
            return result;
        }
        #endregion

        #region 删除学术资源
        /// <summary>
        /// 删除学术资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResopne<string> DelAcademicResources(int id)
        {
            BaseResopne<string> result = new BaseResopne<string>();
            try
            {
                //获取当前登录人
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Error = "获取当前用户信息失败";
                    return result;
                }
                //获取资源信息
                var Resource = GetAcademicResources(id);
                if (Resource == null)
                {
                    result.IsSuccess = false;
                    result.Error = "查询数据不存在";
                    return result;
                }

                result.IsSuccess = DelAcademicResources(Resource);
                if (!result.IsSuccess)
                {
                    result.Error = "数据删除失败";
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除学术资源", "DelAcademicResources", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 删除学术资源
        /// <summary>
        /// 删除学术资源
        /// </summary>
        /// <param name="Resource"></param>
        /// <returns></returns>
        private bool DelAcademicResources(AcademicResources Resource)
        {
            try
            {
                //删除子文章
                if (Resource.isChild)
                {
                    if (DelAcademicResourcesByParentID(Resource.id))
                    {
                        return false;
                    }
                }
                //删除目录
                if (Resource.isMenu)
                {
                    if (DelMenuByParent(Resource.id))
                    {
                        return false;
                    }
                }
                //删除上传文件
                DelResourcesFileByParent(Resource.id);
                //生成sql
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"delete from   de2_aresource where id=@id");
                //执行sql语句
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql.ToString(), new MySqlParameter("@id", Resource.id));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除学术资源", "DelAcademicResources", ex);
                return false;
            }

        }
        #endregion

        #region 根据父级id删除学术资源
        /// <summary>
        /// 根据父级id删除学术资源
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public bool DelAcademicResourcesByParentID(int parentID)
        {
            try
            {
                //获取子级学术资源
                string sql = "select * from de2_aresource where parentID=@parentID";
                //执行sql
                MySqlClient _Client = new MySqlClient();
                var result = _Client.ExecuteQuery<AcademicResources>(sql, new MySqlParameter("@parentID", parentID));
                if (result.Count > 0)
                {
                    foreach (AcademicResources item in result)
                    {
                        if (!DelAcademicResources(item))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "根据父级id删除学术资源", "DelAcademicResourcesByParentID", ex);
            }
            return false;
        }
        #endregion

        #region 保存资源文件
        /// <summary>
        /// 保存资源文件
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public BaseResopne<Int32> SaveResourcesFile(AcademicResources_File Data)
        {
            BaseResopne<Int32> result = new BaseResopne<Int32>();
            try
            {
                //获取当前登录
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Error = "获取当前用户信息失败";
                    return result;
                }
                //判断数据是否存在
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select count(id) from de2_aresource_file where parentID=@parentID");
                MySqlClient _Client = new MySqlClient();
                var count = Convert.ToInt32(_Client.ExecuteScalar(sql.ToString(), new MySqlParameter("@parentID", Data.parentID)));
                if (count == 0)
                {
                    Data.createTime = DateTime.Now;
                    Data.createUserID = user.id;
                    Data.lastModifyTime = DateTime.Now;
                    Data.lastModifyUsrID = user.id;
                    AddResourcesFile(Data);
                }
                else
                {
                    Data.lastModifyTime = DateTime.Now;
                    Data.lastModifyUsrID = user.id;
                    AddResourcesFile(Data);
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "保存资源文件", "SaveResourcesFile", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 新建资源文件
        /// <summary>
        /// 新建资源文件
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        private bool AddResourcesFile(AcademicResources_File Data)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"insert into de2_aresource_file (title,createTime,targetFileUrl,sourceTypeID,parentID,lastModifyTime,lastModifyUsrID) values(@title,@createTime,@targetFileUrl,@sourceTypeID,@parentID,@lastModifyTime,@lastModifyUsrID)");
                //执行sql语句
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql.ToString()
                    , new MySqlParameter("@title", Data.title)
                    , new MySqlParameter("@createTime", Data.createTime)
                    , new MySqlParameter("@targetFileUrl", Data.targetFileUrl)
                    , new MySqlParameter("@sourceTypeID", Data.sourceTypeID)
                    , new MySqlParameter("@parentID", Data.parentID)
                    , new MySqlParameter("@lastModifyTime", Data.lastModifyTime)
                    , new MySqlParameter("@lastModifyUsrID", Data.lastModifyUsrID));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "新建资源文件", "AddResourcesFile", ex);
            }
            return false;
        }
        #endregion

        #region 修改资源文件
        /// <summary>
        /// 修改资源文件
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        private bool UpdateResourcesFile(AcademicResources_File Data)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"update de2_aresource_file set title=@title,createTime=@createTime,targetFileUrl=@targetFileUrl,sourceTypeID=@sourceTypeID ,lastModifyTime=@lastModifyTime,lastModifyUsrID=@lastModifyUsrID where parentID=@parentID");
                //执行sql语句
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql.ToString()
                    , new MySqlParameter("@title", Data.title)
                    , new MySqlParameter("@createTime", Data.createTime)
                    , new MySqlParameter("@targetFileUrl", Data.targetFileUrl)
                    , new MySqlParameter("@sourceTypeID", Data.sourceTypeID)
                    , new MySqlParameter("@parentID", Data.parentID)
                    , new MySqlParameter("@parentID", Data.parentID)
                    , new MySqlParameter("@lastModifyTime", Data.lastModifyTime)
                    , new MySqlParameter("@lastModifyUsrID", Data.lastModifyUsrID));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "新建资源文件", "AddResourcesFile", ex);
            }
            return false;
        }
        #endregion

        #region 删除资源文件
        /// <summary>
        /// 删除资源文件
        /// </summary>
        /// <param name="parentd"></param>
        /// <returns></returns>
        public bool DelResourcesFileByParent(int parentID)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("delete  from de2_aresource_file where parentID=@parentID");
                //执行sql
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql.ToString(), new MySqlParameter("@parentID", parentID));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除资源文件", "DelResourcesFileByParent", ex);
            }
            return false;
        }
        #endregion

        #region 根据父级ID删除目录
        /// <summary>
        /// 根据父级ID删除目录
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private bool DelMenuByParent(int parentID)
        {
            try
            {
                //获取目录列表
                string sql = "select * from  de2_aresource_menu where parentID=@parentID";
                MySqlClient _Client = new MySqlClient();
                var list = _Client.ExecuteQuery<AcademicResources_Menu>(sql, new MySqlParameter("@parentID", parentID));
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (!DelResourceMenu(item.id).IsSuccess)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "根据父级ID删除目录", "DelMenuByParent", ex);
                return false;
            }
            return true;
        }
        #endregion

        #region 新建目录
        /// <summary>
        /// 新建目录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public BaseResopne<Int32> AddResourceMenu(AcademicResources_Menu Param)
        {
            BaseResopne<Int32> result = new BaseResopne<Int32>();
            try
            {
                //获取当前登录人
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Error = "获取当前用户信息失败";
                    return result;
                }
                Param.lastModifyTime = DateTime.Now;
                Param.lastModifyUsrID = user.id;
                Param.createTime = DateTime.Now;
                Param.createUserID = user.id;
                string sql = @"insert into de2_aresource_menu (title,parentID,createTime,createUserID,sourceTypeID,author,lastModifyUsrID,lastModifyTime) VALUES(@title,@parentID,@createTime,@createUserID,@sourceTypeID,@author,@lastModifyUsrID,@lastModifyTime)";
                //执行sql
                MySqlClient _Client = new MySqlClient();
                var ResourceID = _Client.Inster(sql
                    , new MySqlParameter("@title", Param.title)
                    , new MySqlParameter("@parentID", Param.parentID)
                    , new MySqlParameter("@createTime", Param.createTime)
                    , new MySqlParameter("@createUserID", Param.createUserID)
                    , new MySqlParameter("@sourceTypeID", Param.sourceTypeID)
                    , new MySqlParameter("@author", Param.author)
                    , new MySqlParameter("@lastModifyUsrID", Param.lastModifyUsrID)
                    , new MySqlParameter("@lastModifyTime", Param.lastModifyTime));
                //判断文件路径是否为空
                if (!string.IsNullOrEmpty(Param.resourceFile))
                {
                    AcademicResources_File _AcademicResources_File = new AcademicResources_File();
                    _AcademicResources_File.lastModifyTime = DateTime.Now;
                    _AcademicResources_File.lastModifyUsrID = user.id;
                    _AcademicResources_File.createTime = DateTime.Now;
                    _AcademicResources_File.createUserID = user.id;
                    _AcademicResources_File.parentID = Convert.ToInt32(ResourceID);
                    _AcademicResources_File.sourceTypeID = Param.sourceTypeID;
                    _AcademicResources_File.targetFileUrl = Param.resourceFile;
                    _AcademicResources_File.title = Param.title;
                    AddResourcesFile(_AcademicResources_File);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Error = "资源文件路径为空";
                    return result;
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "新建目录", "AddResourceMenu", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 修改目录
        /// <summary>
        /// 修改目录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public BaseResopne<string> UpdateResourceMenu(AcademicResources_Menu Param)
        {
            BaseResopne<string> result = new BaseResopne<string>();
            try
            {
                //获取当前登录人
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Error = "获取当前用户信息失败";
                    return result;
                }
                Param.lastModifyTime = DateTime.Now;
                Param.lastModifyUsrID = user.id;
                string sql = @"update  de2_aresource_menu set title=@title,parentID=@parentID,sourceTypeID=@sourceTypeID,author=@author,lastModifyUsrID=@lastModifyUsrID,lastModifyTime=@lastModifyTime";
                //执行sql
                MySqlClient _Client = new MySqlClient();
                var ResourceID = _Client.Inster(sql
                    , new MySqlParameter("@title", Param.title)
                    , new MySqlParameter("@parentID", Param.parentID)
                    , new MySqlParameter("@sourceTypeID", Param.sourceTypeID)
                    , new MySqlParameter("@author", Param.author)
                    , new MySqlParameter("@lastModifyUsrID", Param.lastModifyUsrID)
                    , new MySqlParameter("@lastModifyTime", Param.lastModifyTime));
                //判断文件路径是否为空
                if (!string.IsNullOrEmpty(Param.resourceFile))
                {
                    AcademicResources_File _AcademicResources_File = new AcademicResources_File();
                    _AcademicResources_File.lastModifyTime = DateTime.Now;
                    _AcademicResources_File.lastModifyUsrID = user.id;
                    _AcademicResources_File.parentID = Param.id;
                    _AcademicResources_File.sourceTypeID = Param.sourceTypeID;
                    _AcademicResources_File.targetFileUrl = Param.resourceFile;
                    _AcademicResources_File.title = Param.title;
                    UpdateResourcesFile(_AcademicResources_File);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Error = "资源文件路径为空";
                    return result;
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改目录", "UpdateResourceMenu", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 删除目录
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResopne<string> DelResourceMenu(int id)
        {

            BaseResopne<string> result = new BaseResopne<string>();
            try
            {
                //删除文件
                if (DelResourcesFileByParent(id))
                {
                    result.IsSuccess = false;
                    result.Error = "资源文件删除失败";
                    return result;
                }
                //删除目录
                string sql = "delete from de2_aresource_menu where id=@id";
                //
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql, new MySqlParameter("@id", id));
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除目录", "DelResourceMenu", ex);
                result.IsSuccess = false;
            }
            return result;
        }
        #endregion

        #region 获取目录详细信息
        /// <summary>
        /// 获取目录详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AcademicResources_Menu GetAcademicResourcesMenuDetail(int id)
        {
            AcademicResources_Menu result = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine(@"select dam.id,dam.title,dam.parentID,dam.createTime,dam.createUserID
                ,u.userName as createUserName,dam.sourceTypeID,dat.typeName as sourceTypeName,dam.author,lastModifyTime,lastModifyUsrID
                from de2_aresource_menu dam
                LEFT JOIN de2_user u on  u.id=dam.createUserID
                LEFT JOIN de2_aresource_type dat on dat.id=dam.sourceTypeID where dam.id=@id");

                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<AcademicResources_Menu>(sql.ToString(), new MySqlParameter("@id", id)).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除目录", "DelResourceMenu", ex);
                return null;
            }
        }
        #endregion

    }
}
