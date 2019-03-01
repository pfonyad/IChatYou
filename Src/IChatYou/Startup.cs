using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IChatYou.Startup))]
namespace IChatYou
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
