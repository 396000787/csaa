using AeronauticalSociety.BusinessLayer.SystemConstant;
using AeronauticalSociety.Log;
using AeronauticalSociety.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    /// <summary>
    /// 关于学会
    /// </summary>
    public class AboutSocietyProvider
    {
        #region 获取取学会简介
        /// <summary>
        /// 获取取学会简介
        /// </summary>
        /// <returns></returns>
        public string GetSocietyIntroduction()
        {
            string result = "";
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetAboutTypeID(Constant.SocietyIntroductionID);
                //
                string sql = "select content from de2_arctype where id=@id ";
                //获取数据
                MySqlClient _MySqlClient = new MySqlClient();
                result = (string)_MySqlClient.ExecuteScalar(sql, new MySqlParameter("id", TypeID));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取取学会简介", "GetSocietyIntroduction", ex);
            }
            return result;
        }
        #endregion

        #region 获取学会章程内容
        /// <summary>
        /// 获取学会章程内容
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public string GetConstitutionDetial()
        {
            string result = "";
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetAboutTypeID(Constant.ConstitutionID);
                //
                string sql = "select content from de2_arctype where id=@id ";
                //获取数据
                MySqlClient _MySqlClient = new MySqlClient();
                result = (string)_MySqlClient.ExecuteScalar(sql, new MySqlParameter("id", TypeID));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学会章程内容", "GetConstitutionDetial", ex);
            }
            return result;
        }
        #endregion

        #region 获取联系方式
        /// <summary>
        /// 获取联系我们电话
        /// </summary>
        /// <returns></returns>
        public Content GetContent()
        {
            Content result = new Content();
            DataCacheProvider _DataCacheProvider = new DataCacheProvider();

            var Contents = _DataCacheProvider.GetCache(Constant.ContentDataJsonFile);
            if (Contents != null)
            {
                result = (Contents as List<Content>).FirstOrDefault();
            }

            return result;
        }
        #endregion

        #region 获取学会荣誉列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public List<Archives> GetHonorList(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetAboutTypeID(Constant.HonorID);
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学会荣誉列表", "GetHonorList", ex);
            }
            return Result;
        }
        #endregion

        #region 获取学会荣誉内容
        /// <summary>
        /// 获取学会荣誉内容
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ArticleDetial GetHonorDetial(int aid)
        {
            ArticleDetial Result = new ArticleDetial();
            try
            {
                NewsProvider _NewsProvider = new NewsProvider();
                return _NewsProvider.GetArticleDetial(aid);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学会荣誉内容", "GetHonorDetial", ex);
            }
            return Result;
        }
        #endregion

        #region 获取表彰管理条例列表
        /// <summary>
        /// 获取表彰管理条例列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetRecognitionAwards(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetNewTypeID(Constant.RecognitionAwardID);

                //获取数据
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取表彰管理条例列表", "GetRecognitionAwards", ex);
            }
            return Result;
        }
        #endregion

        #region 获取学术工作管理条例列表
        /// <summary>
        /// 获取学术工作管理条例列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetAcademicWorks(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetNewTypeID(Constant.AcademicWorkID);

                //获取数据
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术工作管理条例列表", "GetAcademicWorks", ex);
            }
            return Result;
        }
        #endregion

        #region 获取组织条例列表
        /// <summary>
        /// 获取组织条例列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetOrgaincRuleses(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetNewTypeID(Constant.OrgaincRulesID);

                //获取数据
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取组织条例列表", "GetOrgaincRuleses", ex);
            }
            return Result;
        }
        #endregion

        #region 获取管理条例列表
        /// <summary>
        /// 获取管理条例列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetManageRuleses(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetAboutTypeID(Constant.ManageRulesID);

                //获取数据
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取管理条例列表", "GetManageRuleses", ex);
            }
            return Result;
        }
        #endregion

        #region 获取管理条例内容
        /// <summary>
        /// 获取管理条例内容
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ArticleDetial GetManageRulesDetial(int aid)
        {
            ArticleDetial Result = new ArticleDetial();
            try
            {
                NewsProvider _NewsProvider = new NewsProvider();
                return _NewsProvider.GetArticleDetial(aid);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取管理条例内容", "GetManageRulesDetial", ex);
            }
            return Result;
        }
        #endregion

        #region 获取理事会信息
        /// <summary>
        /// 获取理事会信息
        /// </summary>
        /// <returns></returns>
        public string GetCouncil()
        {
            string result = "";
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetOrganizationalTypeID(Constant.CouncilID);
                //
                string sql = "select content from de2_arctype where id=@id ";
                //获取数据
                MySqlClient _MySqlClient = new MySqlClient();
                result = (string)_MySqlClient.ExecuteScalar(sql, new MySqlParameter("id", TypeID));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取理事会信息", "GetCouncil", ex);
            }
            return result;
        }
        #endregion

        #region 获取常务理事会信息
        /// <summary>
        /// 获取常务理事会信息
        /// </summary>
        /// <returns></returns>
        public string GetAffairsCouncil()
        {
            string Result = "";
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetOrganizationalTypeID(Constant.AffairsCouncilID);
                //
                string sql = "select content from de2_arctype where id=@id ";
                //获取数据
                MySqlClient _MySqlClient = new MySqlClient();
                Result = (string)_MySqlClient.ExecuteScalar(sql, new MySqlParameter("id", TypeID));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取常务理事会信息", "GetAffairsCouncil", ex);
            }
            return Result;
        }
        #endregion

        #region 获取工作委员会列表
        /// <summary>
        /// 获取工作委员会列表
        /// </summary>
        /// <returns></returns>
        public List<Archives> GetWorkingCommitteeList(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetOrganizationalTypeID(Constant.WorkingCommitteeID);
                //获取地方学会文章列表
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
                //_Provider.GetAboutTypeID
                //Result = _NewsProvider(Convert.ToInt32(TypeID));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取工作委员会列表", "GetWorkingCommitteeList", ex);
            }
            return Result;
        }
        #endregion

        #region 获取工作委员会
        /// <summary>
        /// 获取工作委员会
        /// </summary>
        /// <returns></returns>
        public ArticleDetial GetWorkingCommittee(int aid)
        {
            ArticleDetial Result = new ArticleDetial();
            try
            {
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetArticleDetial(aid);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取工作委员会", "GetWorkingCommittee", ex);
            }
            return Result;
        }
        #endregion

        #region 获取专业分会列表
        /// <summary>
        /// 获取专业分会列表
        /// </summary>
        /// <returns></returns>
        public List<Archives> GetProfessionalBranchList(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetOrganizationalTypeID(Constant.ProfessionalBranchID);
                //获取地方学会文章列表
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
                //_Provider.GetAboutTypeID
                //Result = _NewsProvider(Convert.ToInt32(TypeID));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专业分会列表", "GetProfessionalBranchList", ex);
            }
            return Result;
        }
        #endregion

        #region 获取专业分会
        /// <summary>
        /// 获取专业分会
        /// </summary>
        /// <returns></returns>
        public ArticleDetial GetProfessionalBranch(int aid)
        {
            ArticleDetial Result = new ArticleDetial();
            try
            {
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetArticleDetial(aid);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专业分会", "GetProfessionalBranch", ex);
            }
            return Result;
        }
        #endregion

        #region 获取地方学会列表
        /// <summary>
        /// 获取地方学会列表
        /// </summary>
        /// <returns></returns>
        public List<Archives> GetLocalAssociationList(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetOrganizationalTypeID(Constant.LocalAssociationID);
                //获取地方学会文章列表
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
                //_Provider.GetAboutTypeID
                //Result = _NewsProvider(Convert.ToInt32(TypeID));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取地方学会列表", "GetLocalAssociationList", ex);
            }
            return Result;
        }
        #endregion

        #region 获取地方学会详情
        /// <summary>
        /// 获取地方学会
        /// </summary>
        /// <returns></returns>
        public ArticleDetial GetLocalAssociation(int aid)
        {
            ArticleDetial Result = new ArticleDetial();
            try
            {
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetArticleDetial(aid);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取地方学会详情", "GetLocalAssociation", ex);
            }
            return Result;
        }
        #endregion

        #region 获取单位学会列表
        /// <summary>
        /// 获取单位学会列表
        /// </summary>
        /// <returns></returns>
        public List<Archives> GetUnitMemberList(int StartRow, int PageSize)
        {
            List<Archives> Result = new List<Archives>();
            try
            {
                //获取分类
                MenuProvider _Provider = new MenuProvider();
                string TypeID = _Provider.GetOrganizationalTypeID(Constant.UnitMemberID);
                //获取地方学会文章列表
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetNewsByTypeID(StartRow, PageSize, TypeID).Data;
                //_Provider.GetAboutTypeID
                //Result = _NewsProvider(Convert.ToInt32(TypeID));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取单位学会列表", "GetUnitMemberList", ex);
            }
            return Result;
        }
        #endregion

        #region 获取单位学会
        /// <summary>
        /// 获取单位学会
        /// </summary>
        /// <returns></returns>
        public ArticleDetial GetUnitMember(int aid)
        {
            ArticleDetial Result = new ArticleDetial();
            try
            {
                NewsProvider _NewsProvider = new NewsProvider();
                Result = _NewsProvider.GetArticleDetial(aid);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取单位学会", "GetUnitMember", ex);
            }
            return Result;
        }
        #endregion
    }
}
