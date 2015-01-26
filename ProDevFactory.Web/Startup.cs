using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProDevFactory.Web.Startup))]
namespace ProDevFactory.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
