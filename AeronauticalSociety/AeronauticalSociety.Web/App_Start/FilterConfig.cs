using AeronauticalSociety.BusinessLayer.Providers;
using System.Web;
using System.Web.Mvc;

namespace AeronauticalSociety.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            
            filters.Add(new HandleErrorAttribute());
        }
    }
}