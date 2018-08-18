using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KlarnaWebAPIClient
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "KlarnaOrder", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Edit",
                url: "klarnaorder/edit/{id}",
                defaults: new { controller = "KlarnaOrder", action = "Edit", id = UrlParameter.Optional }
            );
            
        }
    }
}