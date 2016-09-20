using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocaTile.Startup))]
namespace LocaTile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
