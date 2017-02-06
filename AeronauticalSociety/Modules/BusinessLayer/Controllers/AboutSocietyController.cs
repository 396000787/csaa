using AeronauticalSociety.BusinessLayer.Providers;
using AeronauticalSociety.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace AeronauticalSociety.BusinessLayer.Controllers
{
    /// <summary>
    /// 关于学会
    /// </summary>
    public class AboutSocietyController : ApiController
    {
        #region 获取取学会简介
        /// <summary>
        /// 获取取学会简介
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetSocietyIntroduction()
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetSocietyIntroduction();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取取学会简介", "GetSocietyIntroduction", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取联系我们
        /// <summary>
        /// 获取联系我们电话
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetContent()
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetContent();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取联系我们", "GetContent", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取学会章程内容
        /// <summary>
        /// 获取学会章程内容
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetConstitutionDetial()
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetConstitutionDetial();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学会章程内容", "GetConstitutionDetial", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取学会荣誉列表
        /// <summary>
        /// 获取学会荣誉列表
        /// </summary>
        /// <param name="StartRow">起始行</param>
        /// <param name="PageSize">数据总数</param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage GetHonorList([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetHonorList(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学会荣誉列表", "GetHonorList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region  获取学会荣誉内容
        /// <summary>
        /// 获取学会荣誉内容
        /// </summary>
        /// <param name="aid">文章ID</param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage GetHonorDetial([FromUri]int aid)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetHonorDetial(aid);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学会荣誉内容", "GetHonorDetial", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取表彰管理条例列表
        /// <summary>
        /// 获取表彰管理条例列表
        /// </summary>
        /// <param name="StartRow">起始行</param>
        /// <param name="PageSize">数据总数</param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage GetRecognitionAwards([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetRecognitionAwards(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取表彰管理条例列表", "GetRecognitionAwards", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取学术工作管理条例列表
        /// <summary>
        /// 获取学术工作管理条例列表
        /// </summary>
        /// <param name="StartRow">起始行</param>
        /// <param name="PageSize">数据总数</param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage GetAcademicWorks([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetAcademicWorks(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术工作管理条例列表", "GetAcademicWorks", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取组织条例列表
        /// <summary>
        /// 获取组织条例列表
        /// </summary>
        /// <param name="StartRow">起始行</param>
        /// <param name="PageSize">数据总数</param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage GetOrgaincRuleses([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetOrgaincRuleses(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取组织条例列表", "GetAcademicWorks", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion


        #region 获取管理条例列表
        /// <summary>
        /// 获取管理条例列表
        /// </summary>
        /// <param name="StartRow">起始行</param>
        /// <param name="PageSize">数据总数</param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage GetManageRuleses([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetManageRuleses(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取管理条例列表", "GetAcademicWorks", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region  获取管理条例内容
        /// <summary>
        /// 获取管理条例内容
        /// </summary>
        /// <param name="aid">文章ID</param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage GetManageRulesDetial([FromUri]int aid)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetManageRulesDetial(aid);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取管理条例内容", "GetManageRulesDetial", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion


        #region 获取理事会信息
        /// <summary>
        /// 获取理事会信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetCouncil()
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetCouncil();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取理事会信息", "GetCouncil", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取常务理事会信息
        /// <summary>
        /// 获取常务理事会信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAffairsCouncil()
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetAffairsCouncil();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取常务理事会信息", "GetAffairsCouncil", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取工作委员会列表
        /// <summary>
        /// 获取工作委员会列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetWorkingCommitteeList([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetWorkingCommitteeList(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取工作委员会列表", "GetWorkingCommitteeList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取工作委员会
        /// <summary>
        /// 获取工作委员会
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetWorkingCommittee([FromUri]int aid)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetWorkingCommittee(aid);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取工作委员会", "GetWorkingCommittee", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取专业分会列表
        /// <summary>
        /// 获取专业分会列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetProfessionalBranchList([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetProfessionalBranchList(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专业分会列表", "GetProfessionalBranchList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取专业分会
        /// <summary>
        /// 获取专业分会
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetProfessionalBranch([FromUri]int aid)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetProfessionalBranch(aid);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专业分会", "GetProfessionalBranch", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取地方学会列表
        /// <summary>
        /// 获取地方学会列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetLocalAssociationList([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetLocalAssociationList(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取地方学会列表", "GetLocalAssociationList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取地方学会
        /// <summary>
        /// 获取地方学会
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetLocalAssociation([FromUri]int aid)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetLocalAssociation(aid);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取地方学会", "GetLocalAssociation", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取单位学会列表
        /// <summary>
        /// 获取单位学会列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetUnitMemberList([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetUnitMemberList(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取单位学会列表", "GetUnitMemberList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取单位学会
        /// <summary>
        /// 获取单位学会
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetUnitMember([FromUri]int aid)
        {
            try
            {
                AboutSocietyProvider _Provider = new AboutSocietyProvider();
                var result = _Provider.GetUnitMember(aid);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取单位学会", "GetUnitMember", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

    }
}
