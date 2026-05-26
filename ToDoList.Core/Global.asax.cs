using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using SerilogWeb.Classic.Enrichers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ToDoList.Core
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(new JsonFormatter(), "log-.json", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341/")
                .Enrich.FromLogContext()
                .Enrich.WithHttpRequestId()
                .Enrich.WithHttpRequestRawUrl()
                .Enrich.WithHttpSessionId()
                .Enrich.WithHttpRequestUrl()
                .Enrich.WithUserName()
                .Enrich.WithWebApiActionName()
                .Enrich.WithWebApiControllerName()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .CreateLogger();
        }
    }
}
