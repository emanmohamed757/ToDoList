using System.Web.Http;
using ToDoList.Core.MessageHandlers;

namespace ToDoList.Core
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MessageHandlers.Add(new AddCorrelationIdToLogContextHandler());
            config.MessageHandlers.Add(new AddCorrelationIdToResponseHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
