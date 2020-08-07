using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

//use below modules for json format return
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Net.Http.Headers;
using System.Web.Http.Cors;

namespace TestEmployeeWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           //enable CORS
           
            
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                new MediaTypeHeaderValue("text/html")
            );
           
            EnableCrossSiteRequests(config);
        }
        private static void EnableCrossSiteRequests(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute(
                origins: "http://ecj22:9236,http://localhost:3000,http://localhost:9236",
                headers: "*",
                methods: "*");
            config.EnableCors(cors);
        }
    }
}
