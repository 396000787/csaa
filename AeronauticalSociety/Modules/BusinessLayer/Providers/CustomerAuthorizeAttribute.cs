using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    /// <summary>
    /// 角色属性基类
    /// </summary>
    public class RoleAttribute : Attribute
    {
    }
    /// <summary>
    /// 需要登陆
    /// </summary>
    public class RoleLoginAttrbute : RoleAttribute { }
    /// <summary>
    /// 需要管理员
    /// </summary>
    public class RoleAdminAttrbute : RoleAttribute { }
    /// <summary>
    /// 需要超级管理员
    /// </summary>
    public class RoleSuperAdminAttrbute : RoleAttribute { }
    /// <summary>
    /// 权限
    /// </summary>
    public class CustomerAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            var _Attributes = actionContext.ActionDescriptor.GetCustomAttributes<RoleAttribute>();
            if (_Attributes.Count > 0)
            {
                foreach (var item in _Attributes)
                {
                    if (CheckAuthor(item.GetType().Name))
                    {
                        base.IsAuthorized(actionContext);
                    }
                    else
                    {
                        base.HandleUnauthorizedRequest(actionContext);
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "服务端拒绝访问：你没有权限，或者掉线了");
                    }
                }
            }
        }


        #region 权限验证
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="RoleType"></param>
        /// <returns></returns>
        public bool CheckAuthor(string RoleType)
        {
            //验证是否登陆
            AccountProvider _Provider = new AccountProvider();
            var user = _Provider.GetCurrentUser();
            if (user == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 验证是否登陆
        /// <summary>
        /// 验证是否登陆
        /// </summary>
        /// <returns></returns>
        private bool isLogin()
        {
            return false;
        }
        #endregion

    }
}
