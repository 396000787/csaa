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
    /// 广告
    /// </summary>
    public class AdvertisementController : ApiController
    {
        #region 获取广告列表
        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAdvertisementList([FromUri]int count)
        {
            try
            {
                AdvertisementProvider _Provider = new AdvertisementProvider();
                var result = _Provider.GetAdvertisementList(count);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取广告列表", "GetAdvertisementList", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取广告列表
        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAdvertisementes([FromUri]GetAdvertisementesParam Param)
        {
            try
            {
                AdvertisementProvider _Provider = new AdvertisementProvider();
                var result = _Provider.GetAdvertisementes(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取广告列表", "GetAdvertisementes", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取广告详细信息
        /// <summary>
        /// 获取广告详细信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [RoleAdminAttrbute]
        [HttpGet]
        public HttpResponseMessage GetAdvertisement([FromUri]int key)
        {
            try
            {
                AdvertisementProvider _Provider = new AdvertisementProvider();
                var result = _Provider.GetAdvertisement(key);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取广告详细信息", "GetAdvertisement", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 修改广告信息
        /// <summary>
        /// 修改广告信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [RoleAdminAttrbute]
        [HttpPost]
        public HttpResponseMessage UpdateAdvertisement([FromBody]Advertisement data)
        {
            try
            {
                AdvertisementProvider _Provider = new AdvertisementProvider();
                var result = _Provider.UpdateAdvertisement(data);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改广告信息", "UpdateAdvertisement", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 新建广告信息
        /// <summary>
        /// 新建广告信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [RoleAdminAttrbute]
        [HttpPost]
        public HttpResponseMessage InsterAdvertisement([FromBody]Advertisement data)
        {
            try
            {
                AdvertisementProvider _Provider = new AdvertisementProvider();
                var result = _Provider.InsterAdvertisement(data);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "新建广告信息", "InsterAdvertisement", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 停用广告信息
        /// <summary>
        /// 停用广告信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [RoleAdminAttrbute]
        [HttpGet]
        public HttpResponseMessage StopAdvertisement([FromUri]int key)
        {
            try
            {
                AdvertisementProvider _Provider = new AdvertisementProvider();
                var result = _Provider.StopAdvertisement(key);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "停用广告信息", "StopAdvertisement", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 启用广告信息
        /// <summary>
        /// 启用广告信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [RoleAdminAttrbute]
        [HttpGet]
        public HttpResponseMessage StartAdvertisement([FromUri]int key)
        {
            try
            {
                AdvertisementProvider _Provider = new AdvertisementProvider();
                var result = _Provider.StartAdvertisement(key);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "启用广告信息", "StartAdvertisement", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 删除广告信息
        /// <summary>
        /// 删除广告信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [RoleAdminAttrbute]
        [HttpGet]
        public HttpResponseMessage DelAdvertisement([FromUri]int key)
        {
            try
            {
                AdvertisementProvider _Provider = new AdvertisementProvider();
                var result = _Provider.DelAdvertisement(key);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除广告信息", "DelAdvertisement", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 记录广告点击次数
        /// <summary>
        /// 记录广告点击次数
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage AdvertisementClickCount([FromUri]int key)
        {
            try
            {
                AdvertisementProvider _Provider = new AdvertisementProvider();
                var result = _Provider.AdvertisementClickCount(key);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "记录广告点击次数", "AdvertisementClickCount", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
