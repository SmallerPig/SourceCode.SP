using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SoPhoto
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Partner",
                url: "Partner/{action}/{id}",
                defaults: new { controller = "Partner", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "SoPhoto.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[]{"SoPhoto.Controllers"}
            );
        }
    }
}