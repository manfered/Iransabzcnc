using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Iransabzcnc.WEB.Startup))]
namespace Iransabzcnc.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
