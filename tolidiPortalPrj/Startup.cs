using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tpr.Startup))]
namespace tpr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
