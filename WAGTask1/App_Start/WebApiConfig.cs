using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WAGTask1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PagingActionApi",
                routeTemplate: "api/{controller}/{action}/{page}/{pageSize}/{orderBy}/{orderType}",
                defaults: new { orderBy = "Name", orderType = "ASC", pageSize = 10, page = 1 },
                constraints: new { page = @"\d+", pageSize = @"\d+", orderBy = @"ID|Name|Code", orderType = @"ASC|DESC" }
            );

        }
    }
}
