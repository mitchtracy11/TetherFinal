using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tetherFinal.Startup))]
namespace tetherFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
