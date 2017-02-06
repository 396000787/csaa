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
    public class MenuApiController : ApiController
    {

        #region 获取主菜单
        /// <summary>
        /// 获取主菜单
        /// </summary>      
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetMainMenu()
        {
            try
            {
                MenuProvider _Provider = new MenuProvider();
                var result = _Provider.GetMainMenu().Where(a => a.IsShow);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取主菜单", "GetMainMenu", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取新闻二级菜单
        /// <summary>
        /// 获取新闻二级菜单
        /// </summary>      
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewMenu()
        {
            try
            {
                MenuProvider _Provider = new MenuProvider();
                var result = _Provider.GetNewMenu().Where(a => a.IsShow);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取新闻二级菜单", "GetNewMenu", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取学会动态菜单
        /// <summary>
        /// 获取学会动态菜单
        /// </summary>      
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAssociationMenu()
        {
            try
            {
                MenuProvider _Provider = new MenuProvider();
                var result = _Provider.GetAssociationMenu().Where(a => a.IsShow);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学会动态菜单", "GetAssociationMenu", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取关于学会菜单
        /// <summary>
        /// 获取关于学会菜单
        /// </summary>      
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAboutMenu()
        {
            try
            {
                MenuProvider _Provider = new MenuProvider();
                var result = _Provider.GetAboutMenu().Where(a => a.IsShow);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取关于学会菜单", "GetAboutMenu", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取管理条例二级目录
        /// <summary>
        /// 获取管理条例二级目录
        /// </summary>      
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetManagementRegulations()
        {
            try
            {
                MenuProvider _Provider = new MenuProvider();
                var result = _Provider.GetManagementRegulations().Where(a => a.IsShow);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取管理条例二级目录", "GetManagementRegulations", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取组织机构分类列表
        /// <summary>
        /// 获取组织机构分类列表
        /// </summary>      
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetOrganizationalTypes()
        {
            try
            {
                MenuProvider _Provider = new MenuProvider();
                var result = _Provider.GetOrganizationalTypes().Where(a => a.IsShow);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取组织机构分类列表", "GetOrganizationalTypes", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取管理条例列表
        /// <summary>
        /// 获取管理条例列表
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage GetManageRuleses()
        {
            try
            {
                MenuProvider _Provider = new MenuProvider();
                var result = _Provider.GetManageRuleses().Where(a => a.IsShow);
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

        #region 获取学术资源菜单列表
        /// <summary>
        /// 获取学术资源菜单列表
        /// </summary>      
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAcademicResource()
        {
            try
            {
                MenuProvider _Provider = new MenuProvider();
                var result = _Provider.GetAcademicResource().Where(a => a.IsShow);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术资源菜单列表", "GetAcademicResource", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

    }
}
