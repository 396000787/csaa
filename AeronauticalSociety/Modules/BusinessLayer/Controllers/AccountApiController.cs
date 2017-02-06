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
    public class AccountApiController : ApiController
    {
        #region 登录认证
        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CheckIn([FromBody]LoginInParam Param)
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.CheckIn(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "登录认证", "CheckIn", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 管理员登陆认证
        /// <summary>
        /// 管理员登陆认证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AdminCheckIn([FromBody]LoginInParam Param)
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.AdminCheckIn(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "管理员登陆认证", "AdminCheckIn", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region  用户注册
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RegUser([FromBody]User Param)
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.RegUser(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "用户注册", "RegUser", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
         [RoleLoginAttrbute]
        [HttpPost]
        public HttpResponseMessage UpdatePassword([FromBody]UpdatePasswordParam Param)
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.UpdatePassword(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改密码", "UpdatePassword", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [RoleLoginAttrbute]
        [HttpPost]
        public HttpResponseMessage UpdateMember([FromBody]Member Param)
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.UpdateMember(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改用户信息", "UpdateMember", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取用户详细信息
        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [RoleLoginAttrbute]
        [HttpGet]
        public HttpResponseMessage GetMember([FromUri]string Param)
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.GetMember(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取用户详细信息", "GetMember", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取所在地区
        /// <summary>
        /// 获取所在地区
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [RoleLoginAttrbute]
        [HttpGet]
        public HttpResponseMessage GetAreaList()
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.GetAreaList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取所在地区", "GetAreaList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取专业分会
        /// <summary>
        /// 获取专业分会
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetBranchList()
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.GetBranchList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专业分会", "GetBranchList", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 验证用户名是否存在
        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage LoginNameIsRepeat([FromUri]string Param)
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.LoginNameIsRepeat(Param, Guid.Empty);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "验证用户名是否存在", "LoginNameIsRepeat", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 注册用户绑定
        /// <summary>
        /// 注册用户绑定
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage BindMember([FromBody]BindMemberParam Param)
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.BindMember(Param);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "注册用户绑定", "BindMember", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 退出登录
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckOut()
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.CheckOut();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "退出登录", "CheckOut", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取专业列表
        /// <summary>
        /// 获取专业列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetProfessionList()
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.GetProfessionList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专业列表", "GetProfessionList", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region 获取职业列表
        /// <summary>
        /// 获取职业列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetOccupationList()
        {
            try
            {
                AccountProvider _Provider = new AccountProvider();
                var result = _Provider.GetOccupationList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取职业列表", "GetOccupationList", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
