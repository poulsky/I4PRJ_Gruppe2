using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BargainBarterV2.Startup))]
namespace BargainBarterV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
