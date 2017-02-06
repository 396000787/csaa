using AeronauticalSociety.BusinessLayer.Providers;
using AeronauticalSociety.Log;
using AeronauticalSociety.Model;
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
    /// 学会资源
    /// </summary>
    public class AcademicResourcesController : ApiController
    {
        #region 获取学术资源列表 应用
        /// <summary>
        /// 获取学术资源列表 应用
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAcademicResourcesListForApp([FromUri]GetPeriodicalListParam Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.GetAcademicResourcesListForApp(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术资源列表 应用", "GetAcademicResourcesListForApp", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取学术资源列表 管理端
        /// <summary>
        /// 获取学术资源列表 管理端
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAcademicResourcesListForManager([FromUri]GetPeriodicalListParam Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.GetAcademicResourcesListForManager(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术资源列表 管理端", "GetAcademicResourcesListForManager", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取目录
        /// <summary>
        /// 获取目录
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAcademicResourcesMenu([FromUri]GetAcademicResourcesMenuParam Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.GetAcademicResourcesMenu(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取目录", "GetAcademicResourcesMenu", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取学术资源详细
        /// <summary>
        /// 获取学术资源详细
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAcademicResourcesMenu([FromUri]int parentID)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.GetAcademicResourcesFile(parentID);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术资源详细", "GetAcademicResourcesFile", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 保存学术资源信息
        /// <summary>
        /// 保存学术资源信息
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpPost]
        public HttpResponseMessage AddAcademicResources([FromBody]AcademicResources Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.AddAcademicResources(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "保存学术资源信息", "AddAcademicResources", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 修改学术资源信息
        /// <summary>
        /// 修改学术资源信息
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpPost]
        public HttpResponseMessage UpdateAcademicResources([FromBody]AcademicResources Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.UpdateAcademicResources(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改学术资源信息", "UpdateAcademicResources", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 修改学术资源发布状态
        /// <summary>
        /// 修改学术资源发布状态
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpPost]
        public HttpResponseMessage UpdatePublishStatue([FromBody]AcademicResources Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.UpdatePublishStatue(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改学术资源发布状态", "UpdateAcademicResources", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 删除学术资源
        /// <summary>
        /// 删除学术资源
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpGet]
        public HttpResponseMessage DelAcademicResources([FromUri]int Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.DelAcademicResources(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除学术资源", "DelAcademicResources", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 根据父级id删除学术资源
        /// <summary>
        /// 根据父级id删除学术资源
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpGet]
        public HttpResponseMessage DelAcademicResourcesByParentID([FromUri]int Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.DelAcademicResourcesByParentID(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "根据父级id删除学术资源", "DelAcademicResourcesByParentID", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 保存资源文件
        /// <summary>
        /// 保存资源文件
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpPost]
        public HttpResponseMessage SaveResourcesFile([FromBody]AcademicResources_File Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.SaveResourcesFile(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "保存资源文件", "SaveResourcesFile", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 删除资源文件
        /// <summary>
        /// 删除资源文件
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpGet]
        public HttpResponseMessage DelResourcesFileByParent([FromUri]int Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.DelResourcesFileByParent(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除资源文件", "DelResourcesFileByParent", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 新建目录
        /// <summary>
        /// 新建目录
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpPost]
        public HttpResponseMessage AddResourceMenu([FromBody]AcademicResources_Menu Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.AddResourceMenu(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "新建目录", "AddResourceMenu", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 修改目录
        /// <summary>
        /// 修改目录
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpPost]
        public HttpResponseMessage UpdateResourceMenu([FromBody]AcademicResources_Menu Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.UpdateResourceMenu(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改目录", "UpdateResourceMenu", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 删除目录
        /// <summary>
        /// 删除目录
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpGet]
        public HttpResponseMessage DelResourceMenu([FromUri]int Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.DelResourceMenu(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除目录", "DelResourceMenu", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取目录详细信息
        /// <summary>
        /// 获取目录详细信息
        /// </summary>        
        /// <returns></returns>
        [RoleLoginAttrbute]
        [RoleAdminAttrbute]
        [HttpGet]
        public HttpResponseMessage GetAcademicResourcesMenuDetail([FromUri]int Param)
        {
            try
            {
                AcademicResourcesProvider _Provider = new AcademicResourcesProvider();
                var result = _Provider.GetAcademicResourcesMenuDetail(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取目录详细信息", "GetAcademicResourcesMenuDetail", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
