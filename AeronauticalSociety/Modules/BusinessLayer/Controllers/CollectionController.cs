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
    /// 收藏文章
    /// </summary>
    public class CollectionController : ApiController
    {
        #region 添加收藏
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddColletion([FromBody]Collection Param)
        {
            try
            {
                CollectionProvider _Provider = new CollectionProvider();
                var result = _Provider.AddColletion(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "添加收藏", "AddColletion", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取收藏列表
        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetCollectionList([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                CollectionProvider _Provider = new CollectionProvider();
                var result = _Provider.GetCollectionList(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取收藏列表", "GetCollectionList", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 删除收藏
        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage DelColletion([FromUri]int Param)
        {
            try
            {
                CollectionProvider _Provider = new CollectionProvider();
                var result = _Provider.DelColletion(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除收藏", "DelColletion", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 取消收藏
        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="Param">文章ID</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CancelColletion([FromUri]int Param)
        {
            try
            {
                CollectionProvider _Provider = new CollectionProvider();
                var result = _Provider.CancelColletion(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "取消收藏", "CancelColletion", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
