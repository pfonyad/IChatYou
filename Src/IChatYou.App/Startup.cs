using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IChatYou.App.Startup))]
namespace IChatYou.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
