using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApiAdventure.Startup))]
namespace WebApiAdventure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
