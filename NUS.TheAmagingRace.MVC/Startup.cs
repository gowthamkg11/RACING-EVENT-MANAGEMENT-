using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NUS.TheAmagingRace.MVC.Startup))]
namespace NUS.TheAmagingRace.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
