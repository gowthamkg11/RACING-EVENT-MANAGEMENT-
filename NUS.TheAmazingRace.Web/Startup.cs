using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NUS.TheAmazingRace.Web.Startup))]
namespace NUS.TheAmazingRace.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
