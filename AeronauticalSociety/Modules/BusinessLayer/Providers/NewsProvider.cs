using AeronauticalSociety.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AeronauticalSociety.Log;
using TinyStack.Modules.iSqlHelper;
using AeronauticalSociety.BusinessLayer.SystemConstant;
using MySql.Data.MySqlClient;
using AeronauticalSociety.BusinessLayer.Controllers;


namespace AeronauticalSociety.BusinessLayer.Providers
{

    #region 根据新闻类型获取新闻列表参数
    /// <summary>
    /// 根据新闻类型获取新闻列表参数
    /// </summary>
    public class GetNewsByTypeIDParam
    {
        public int StartRow { get; set; }
        public int PageSize { get; set; }
        public string TypeID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string KeyWord { get; set; }
    }
    #endregion


    #region 根据检索关键字获取新闻列表参数
    /// <summary>
    /// 根据检索关键字获取新闻列表参数
    /// </summary>
    public class GetNewsBySearchKeyParam
    {
        public int StartRow { get; set; }
        public int PageSize { get; set; }
        public string TypeID { get; set; }
        public string SearchKey { get; set; }

    }
    #endregion
    /// <summary>
    /// 新闻
    /// </summary>
    public class NewsProvider
    {
        #region 获取头条新闻
        /// <summary>
        /// 获取头条新闻
        /// </summary>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<Archives> GetHeadline(int Count)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                log.SaveLog("开始获取数据！");
                AccountProvider _AccountProvider = new AccountProvider();
                //获取访问权限
                int VewBrowsePermissions = _AccountProvider.GetBrowsePermissions();
                //生成检索语句
                string sql = string.Format("select * from de2_archives where  flag like '%h%' and arcrank!=-1 and( arcrank=0 or arcrank={0} )order by sortrank desc limit @limit", VewBrowsePermissions);

                MySqlClient _client = new MySqlClient();
                log.SaveLog("开始执行SQL！");
                Result = _client.ExecuteQuery<Archives>(sql, new MySqlParameter("@limit", Count));
                log.SaveLog("完成SQL！");
                Result = FromateLitpic(Result);
            }
            catch (Exception ex)
            {
                log.SaveLog("执行错误！" + ex.Message);
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取头条新闻", "GetHeadline", ex);
            }
            return Result;
        }
        #endregion

        #region 格式化图片地址
        /// <summary>
        /// 格式化图片地址
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public List<Archives> FromateLitpic(List<Archives> Data)
        {

            //读取配置文件
            string imgPath = Constant.DefaultImage;

            if (Data == null || Data.Count == 0)
            {
                return Data;
            }
            foreach (var item in Data)
            {
                if (string.IsNullOrEmpty(item.litpic))
                {
                    item.litpic = imgPath;
                }
                else
                {
                    if (item.litpic.ToLower().IndexOf("http://") == -1 && item.litpic.ToLower().IndexOf("https://") == -1)
                    {
                        item.litpic = Constant.csaaAddress + item.litpic;
                    }
                }
            }
            return Data;
        }
        #endregion

        #region 根据新闻类型获取新闻列表
        /// <summary>
        /// 根据新闻类型获取新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <param name="TypeID"></param>
        /// <param name="Author"></param>
        /// <returns></returns>
        public BaseResopne<List<Archives>> GetNewsByTypeID(int StartRow, int PageSize, string TypeID, string Author = "", string Title = "", string KeyWord = "")
        {
            BaseResopne<List<Archives>> Result = new BaseResopne<List<Archives>>();
            try
            {
                AccountProvider _AccountProvider = new AccountProvider();
                //获取访问权限
                int VewBrowsePermissions = _AccountProvider.GetBrowsePermissions();

                //生成检索语句
                StringBuilder sql = new StringBuilder();
                StringBuilder countsql = new StringBuilder();
                sql.AppendLine("select * from de2_archives where arcrank!=-1 and ( arcrank=0 or arcrank=@Permissions)");
                countsql.AppendLine("select count(id) from de2_archives where arcrank!=-1 and ( arcrank=0 or arcrank=@Permissions)");
                if (!string.IsNullOrEmpty(TypeID))
                {
                    if (TypeID.IndexOf(",") > 0)
                    {
                        sql.AppendLine("and typeid in (" + TypeID + ")");
                        countsql.AppendLine("and typeid in (" + TypeID + ")");
                    }
                    else
                    {
                        sql.AppendLine(" and typeid=@typeid ");
                        countsql.AppendLine(" and typeid=@typeid ");
                    }
                }
                if (!string.IsNullOrEmpty(Author))
                {
                    sql.AppendLine(" and writer = @writer ");
                    countsql.AppendLine(" and writer = @writer ");
                }
                if (!string.IsNullOrEmpty(Title))
                {
                    sql.AppendLine(" and title like @title ");
                    countsql.AppendLine(" and title like @title ");
                }

                if (!string.IsNullOrEmpty(KeyWord))
                {
                    sql.AppendLine(" and keywords like @keywordes ");
                    countsql.AppendLine(" and keywords like @keywordes ");
                }
                //if (!string.IsNullOrEmpty(where))
                //{
                //    sql.AppendLine("&& typeid in @typeIn");
                //}
                sql.AppendLine(" order by sortrank desc");
                sql.AppendLine(" limit @start,@count ");
                MySqlClient _client = new MySqlClient();
                Result.Total = Convert.ToInt32(_client.ExecuteScalar(countsql.ToString(),
                    new MySqlParameter("@start", StartRow),
                    new MySqlParameter("@count", PageSize),
                    new MySqlParameter("@Permissions", VewBrowsePermissions),
                    new MySqlParameter("@typeid", TypeID),
                    new MySqlParameter("@title", "%" + Title + "%"),
                    new MySqlParameter("@keywordes", "%" + KeyWord + "%"),
                    new MySqlParameter("@writer", Author)));

                Result.Data = _client.ExecuteQuery<Archives>(sql.ToString(),
                    new MySqlParameter("@start", StartRow),
                    new MySqlParameter("@count", PageSize),
                    new MySqlParameter("@Permissions", VewBrowsePermissions),
                    new MySqlParameter("@typeid", TypeID),
                    new MySqlParameter("@title", "%" + Title + "%"),
                    new MySqlParameter("@keywordes", "%" + KeyWord + "%"),
                    new MySqlParameter("@writer", Author));

                Result.Data = FromateLitpic(Result.Data);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "根据新闻类型获取新闻列表", "GetNewsByTypeID", ex);
                Result.IsSuccess = false;
                Result.Error = ex.Message;
            }
            return Result;
        }
        #endregion


        #region 根据新闻类型获取新闻列表
        /// <summary>
        /// 根据新闻类型获取新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <param name="TypeID"></param>
        /// <param name="Author"></param>
        /// <returns></returns>
        public BaseResopne<List<Archives>> GetNewsByTypeID(int StartRow, int PageSize, string TypeID, string SearchKey)
        {
            BaseResopne<List<Archives>> Result = new BaseResopne<List<Archives>>();
            try
            {
                AccountProvider _AccountProvider = new AccountProvider();
                //获取访问权限
                int VewBrowsePermissions = _AccountProvider.GetBrowsePermissions();

                //生成检索语句
                StringBuilder sql = new StringBuilder();
                StringBuilder countsql = new StringBuilder();
                sql.AppendLine("select * from de2_archives where arcrank!=-1 and ( arcrank=0 or arcrank=@Permissions)");
                countsql.AppendLine("select count(id) from de2_archives where arcrank!=-1 and ( arcrank=0 or arcrank=@Permissions)");
                if (!string.IsNullOrEmpty(TypeID))
                {
                    if (TypeID.IndexOf(",") > 0)
                    {
                        sql.AppendLine("and typeid in (" + TypeID + ")");
                        countsql.AppendLine("and typeid in (" + TypeID + ")");
                    }
                    else
                    {
                        sql.AppendLine(" and typeid=@typeid ");
                        countsql.AppendLine(" and typeid=@typeid ");
                    }
                }
                if (!string.IsNullOrEmpty(SearchKey))
                {
                    sql.AppendLine(" and (writer like @SearchKey or  title like @SearchKey or  keywords like @SearchKey)");
                    countsql.AppendLine(" and (writer like @SearchKey or  title like @SearchKey or  keywords like @SearchKey) ");
                }

                sql.AppendLine(" order by sortrank desc");
                sql.AppendLine(" limit @start,@count ");
                MySqlClient _client = new MySqlClient();
                Result.Total = Convert.ToInt32(_client.ExecuteScalar(countsql.ToString(),
                    new MySqlParameter("@start", StartRow),
                    new MySqlParameter("@count", PageSize),
                    new MySqlParameter("@Permissions", VewBrowsePermissions),
                    new MySqlParameter("@typeid", TypeID),
                    new MySqlParameter("@SearchKey", "%" + SearchKey + "%")));

                Result.Data = _client.ExecuteQuery<Archives>(sql.ToString(),
                    new MySqlParameter("@start", StartRow),
                    new MySqlParameter("@count", PageSize),
                    new MySqlParameter("@Permissions", VewBrowsePermissions),
                    new MySqlParameter("@typeid", TypeID),
                    new MySqlParameter("@SearchKey", "%" + SearchKey + "%"));

                Result.Data = FromateLitpic(Result.Data);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "根据新闻类型获取新闻列表", "GetNewsByTypeID", ex);
                Result.IsSuccess = false;
                Result.Error = ex.Message;
            }
            return Result;
        }
        #endregion

        #region  获取收藏列表
        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="StartRow">开始行</param>
        /// <param name="PageSize">页面数据大小</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<Archives> GetCollectionList(int StartRow, int PageSize, int userID)
        {
            List<Archives> Result = new List<Archives>();
            try
            {

                //生成检索语句
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select ar.* from de2_archives as ar join  de2_collection as co on ar.id=co.aid  where co.createID=@userID");

                sql.AppendLine(" order by sortrank desc");
                sql.AppendLine(" limit @start,@count ");
                MySqlClient _client = new MySqlClient();

                Result = _client.ExecuteQuery<Archives>(sql.ToString(),
                    new MySqlParameter("@start", StartRow),
                    new MySqlParameter("@count", PageSize),
                    new MySqlParameter("@userID", userID));
                Result = FromateLitpic(Result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取收藏列表", "GetCollectionList", ex);
            }
            return Result;
        }
        #endregion

        #region 获取普通新闻列表
        /// <summary>
        /// 获取普通新闻列表
        /// </summary>
        /// <returns></returns>
        public List<Archives> GetTextNews(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetNewTypeID(Constant.TextNewsID);
                //获取数据
                Result = GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取普通新闻列表", "GetTextNews", ex);
            }
            return Result;
        }
        #endregion

        #region 获取图片新闻列表
        /// <summary>
        /// 获取图片新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetImageNews(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetNewTypeID(Constant.TextNewsID);
                //获取数据
                Result = GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取图片新闻列表", "GetTextNews", ex);
            }
            return Result;
        }
        #endregion

        #region 获取视频新闻列表
        /// <summary>
        /// 获取视频新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetVideoNews(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetNewTypeID(Constant.VideoNewsID);
                //获取数据
                Result = GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取视频新闻列表", "GetVideoNews", ex);
            }
            return Result;
        }
        #endregion

        #region 获取专题新闻列表
        /// <summary>
        /// 获取专题新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetColumnsNews(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetNewTypeID(Constant.ColumnsID);
                //获取数据
                Result = GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专题新闻列表", "GetColumnsNews", ex);
            }
            return Result;
        }
        #endregion

        #region 获取学会工作
        /// <summary>
        /// 获取学会工作
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetAssociation(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetAssociationTypeID(Constant.AssociationID);
                //获取数据
                Result = GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专题新闻列表", "GetColumnsNews", ex);
            }
            return Result;
        }
        #endregion

        #region 获取通知通告
        /// <summary>
        /// 获取通知通告
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetNotices(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetAssociationTypeID(Constant.NoticeID);
                //获取数据
                Result = GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取通知通告", "GetNotices", ex);
            }
            return Result;
        }
        #endregion

        #region 学会活动
        /// <summary>
        /// 学会活动
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetActions(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetAssociationTypeID(Constant.ActionID);
                //获取数据
                Result = GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "学会活动", "GetActions", ex);
            }
            return Result;
        }
        #endregion

        #region 获取文章详情
        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <returns></returns>
        public ArticleDetial GetArticleDetial(int aid)
        {
            ArticleDetial result = new ArticleDetial();
            try
            {
                result.BaseInfro = GetArchives(aid);
                result.Body = GetAddonarticle(aid);
                //修改点击次数
                UpdataClickCount(aid);
                //获取当前登录人
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null)
                {
                    return result;
                }
                //验证是否被关注
                result.isFouce = IsFocues(result.BaseInfro.writer, user.id);
                //验证是否被收藏
                result.isCollection = IsCollection(result.BaseInfro.id, user.id);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取文章详情", "GetArticleDetial", ex);
            }
            return result;
        }
        #endregion

        #region 验证作者是否被关注
        /// <summary>
        /// 验证作者是否被关注
        /// </summary>
        /// <param name="author">作者</param>
        /// <param name="userid">当前用户</param>
        /// <returns></returns>
        public bool IsFocues(string author, int userid)
        {
            if (string.IsNullOrEmpty(author))
            {
                return false;
            }
            try
            {
                string sql = "select count(id) from de2_concerns where authorName=@authorName and createID=@createID";
                MySqlClient _Client = new MySqlClient();
                var count = Convert.ToInt32(_Client.ExecuteScalar(sql, new MySqlParameter("@authorName", author), new MySqlParameter("@createID", userid)));
                if (count > 0)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "验证作者是否被关注", "IsFocues", ex);
            }
            return false;
        }
        #endregion

        #region 验证文章是否被收藏
        /// <summary>
        /// 验证文章是否被收藏
        /// </summary>
        /// <param name="aid">文章ID</param>
        /// <param name="userid">当前用户</param>
        /// <returns></returns>
        public bool IsCollection(int aid, int userid)
        {

            try
            {
                string sql = "select count(id) from de2_collection where aid=@aid and createID=@createID";
                MySqlClient _Client = new MySqlClient();
                var count = Convert.ToInt32(_Client.ExecuteScalar(sql, new MySqlParameter("@aid", aid), new MySqlParameter("@createID", userid)));
                if (count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "验证文章是否被收藏", "IsCollection", ex);
            }
            return false;
        }
        #endregion

        #region 修改点击次数
        /// <summary>
        /// 修改点击次数
        /// </summary>
        /// <param name="aid"></param>
        public void UpdataClickCount(int aid)
        {
            try
            {
                string sql = "update de2_archives set click=click+1 where id=@id";
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql.ToString(), new MySqlParameter("@id", aid));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改点击次数", "UpdataClickCount", ex);
            }
        }
        #endregion

        #region 获取文章配置信息
        /// <summary>
        /// 获取文章配置信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public Archives GetArchives(int aid)
        {
            Archives result = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select * from de2_archives where id=@id");
                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<Archives>(sql.ToString(), new MySqlParameter("@id", aid)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取文章配置信息", "GetArchives", ex);
            }
            return result;
        }
        #endregion

        #region 获取文章内容信息
        /// <summary>
        /// 获取文章内容信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public Addonarticle GetAddonarticle(int aid)
        {
            Addonarticle result = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select * from de2_addonarticle where aid=@id");
                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<Addonarticle>(sql.ToString(), new MySqlParameter("@id", aid)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取文件内容信息", "GetAddonarticle", ex);
            }
            return result;
        }
        #endregion


    }
}
