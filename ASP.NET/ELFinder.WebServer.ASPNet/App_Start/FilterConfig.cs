using System.Web;
using System.Web.Mvc;

namespace ELFinder.WebServer.ASPNet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
