using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(data_validation.net.Web.Startup))]
namespace data_validation.net.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
