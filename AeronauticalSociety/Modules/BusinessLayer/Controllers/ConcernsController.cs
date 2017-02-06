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
    /// 作者关注
    /// </summary>
    public class ConcernsController : ApiController
    {
        #region 添加关注
        /// <summary>
        /// 添加关注
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddConcerns([FromBody]Concerns Param)
        {
            try
            {
                ConcernsProvider _Provider = new ConcernsProvider();
                var result = _Provider.AddConcerns(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "添加关注", "AddConcerns", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取关注作者列表
        /// <summary>
        /// 获取关注作者列表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetConcernsList([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                ConcernsProvider _Provider = new ConcernsProvider();
                var result = _Provider.GetConcernsList(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取关注作者列表", "GetCollectionList", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 删除关注
        /// <summary>
        /// 删除关注
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage DelConcerns([FromUri]int Param)
        {
            try
            {
                ConcernsProvider _Provider = new ConcernsProvider();
                var result = _Provider.DelConcerns(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除关注", "DelConcerns", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 取消关注
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="Param">文章作者</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CancelConcerns([FromUri]string Param)
        {
            try
            {
                ConcernsProvider _Provider = new ConcernsProvider();
                var result = _Provider.CancelConcerns(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "取消关注", "CancelConcerns", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
