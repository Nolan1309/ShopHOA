using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopBanHoa
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
          
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Cấu hình route mặc định cho trang quản trị Admin
            //routes.MapRoute(
            //    name: "AdminDefault",
            //    url: "Admin/{controller}/{action}/{id}",
            //    defaults: new { area = "Admin", controller = "Home", action = "Index", id = UrlParameter.Optional },
            //     namespaces: new[] { "ShopBanHoa.Admin.Controllers" }
            //);

            // Các route khác ở đây

            //Route mặc định cho phần khác của ứng dụng
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopBanHoa.Controllers" }
            );
        }
    }
}
