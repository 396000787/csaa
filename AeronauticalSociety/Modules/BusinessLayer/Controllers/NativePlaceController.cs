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
    /// 省市地区
    /// </summary>
    public class NativePlaceController : ApiController
    {
        #region 获取省列表
        /// <summary>
        /// 获取省列表
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetProvinceList()
        {
            try
            {
                NativePlaceProvider _Provider = new NativePlaceProvider();
                var result = _Provider.GetProvinceList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取省列表", "GetProvinceList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 根据省ID 获取市
        /// <summary>
        /// 根据省ID 获取市
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetCityList([FromUri]string ParentID)
        {
            try
            {
                NativePlaceProvider _Provider = new NativePlaceProvider();
                var result = _Provider.GetCityList(ParentID);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "根据省ID 获取市", "GetCityList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
