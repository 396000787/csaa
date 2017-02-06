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
    /// 工作计划
    /// </summary>
    public class WorkPlanController : ApiController
    {
        #region 获取年度工作计划
        /// <summary>
        /// 获取年度工作计划
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetWorkPlanList([FromUri]string Year, [FromUri]string Month, [FromUri]string StartRow, [FromUri]string PageSize, [FromUri]string Title, [FromUri]string StartTime, [FromUri]string EndTime)
        {
            try
            {
                WorkPlanProider _Provider = new WorkPlanProider();
                var result = _Provider.GetWorkPlanList(Year, Month, StartRow, PageSize, Title, StartTime, EndTime);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取年度工作计划", "GetWorkPlanList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取年度计划详情
        /// <summary>
        /// 获取年度计划详情
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetWorkPlanDetial([FromUri]int wid)
        {
            try
            {
                WorkPlanProider _Provider = new WorkPlanProider();
                var result = _Provider.GetWorkPlanDetial(wid);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取年度计划详情", "GetWorkPlanDetial", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 新建工作计划
        /// <summary>
        /// 新建工作计划
        /// </summary>
        /// <param name="Data">工作计划数据</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddWorkPlan([FromBody]WorkPlan Data)
        {
            try
            {
                WorkPlanProider _Provider = new WorkPlanProider();
                var result = _Provider.AddWorkPlan(Data);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "新建工作计划", "AddWorkPlan", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region  修改工作计划
        /// <summary>
        /// 修改工作计划
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage UpdateWorkPlan([FromBody]WorkPlan Data)
        {
            try
            {
                WorkPlanProider _Provider = new WorkPlanProider();
                var result = _Provider.UpdateWorkPlan(Data);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改工作计划", "UpdateWorkPlan", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region  删除工作计划
        /// <summary>
        /// 删除工作计划
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage DelWorkPlan([FromUri]int wid)
        {
            try
            {
                WorkPlanProider _Provider = new WorkPlanProider();
                var result = _Provider.DelWorkPlan(wid);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除工作计划", "DelWorkPlan", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
