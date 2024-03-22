using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CT_Store.Startup))]
namespace CT_Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
