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
    /// 新闻
    /// </summary>
    public class NewApiController : ApiController
    {
        #region 获取头条新闻
        /// <summary>
        /// 获取头条新闻
        /// </summary>
        /// <param name="Count"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetHeadline([FromUri]Int32 Count)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetHeadline(Count);
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取头条新闻", "GetHeadline", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 根据新闻类型获取新闻列表
        /// <summary>
        /// 根据新闻类型获取新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <param name="TypeID"></param>
        /// <param name="Author">作者</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewsByTypeID([FromUri]GetNewsByTypeIDParam Param)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetNewsByTypeID(Param.StartRow, Param.PageSize, Param.TypeID, Param.Author, Param.Title, Param.KeyWord);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "根据新闻类型获取新闻列表", "GetNewsByTypeID", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #endregion

        #region 根据检索关键字获取新闻列表
        /// <summary>
        /// 根据检索关键字获取新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <param name="TypeID"></param>
        /// <param name="GetNewsBySearchKey">作者</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewsBySearchKey([FromUri]GetNewsBySearchKeyParam Param)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetNewsByTypeID(Param.StartRow, Param.PageSize, Param.TypeID, Param.SearchKey);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "根据检索关键字获取新闻列表", "GetNewsBySearchKey", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取普通新闻列表
        /// <summary>
        /// 获取普通新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetTextNews([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetTextNews(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取普通新闻列表", "GetTextNews", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取图片新闻列表
        /// <summary>
        /// 获取图片新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetImageNews([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetImageNews(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取图片新闻列表", "GetImageNews", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取视频新闻列表
        /// <summary>
        /// 获取视频新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetVideoNews([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetVideoNews(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取视频新闻列表", "GetVideoNews", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取专题新闻列表
        /// <summary>
        /// 获取专题新闻列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetColumnsNews([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetColumnsNews(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专题新闻列表", "GetColumnsNews", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取文件内容信息
        /// <summary>
        /// 获取文件内容信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetTextNewsDetial([FromUri]int aid)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetArticleDetial(aid);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取文件内容信息", "GetTextNewsDetial", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取学会工作
        /// <summary>
        /// 获取学会工作
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAssociation([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetAssociation(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学会工作", "GetAssociation", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取通知通告
        /// <summary>
        /// 获取通知通告
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNotices([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetNotices(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取通知通告", "GetNotices", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 学会活动
        /// <summary>
        /// 学会活动
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>        
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetActions([FromUri]int StartRow, [FromUri]int PageSize)
        {
            try
            {
                NewsProvider _Provider = new NewsProvider();
                var result = _Provider.GetActions(StartRow, PageSize);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "学会活动", "GetActions", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion


    }
}