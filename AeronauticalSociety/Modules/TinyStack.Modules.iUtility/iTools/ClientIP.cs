using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TinyStack.Modules.iUtility.iTools
{
    public class ClientIP
    {       
        public static string GetClientIP()
        {
            string realRemoteIP = "";
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                realRemoteIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0];
            }
            if (string.IsNullOrEmpty(realRemoteIP))
            {
                realRemoteIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return realRemoteIP;
        }
    }
   

}
