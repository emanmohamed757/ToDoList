using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoList.Web.Startup))]
namespace ToDoList.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
