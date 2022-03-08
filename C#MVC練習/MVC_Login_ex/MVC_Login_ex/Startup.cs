using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Login_ex.Startup))]
namespace MVC_Login_ex
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
