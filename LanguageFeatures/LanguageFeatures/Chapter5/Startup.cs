using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chapter5.Startup))]
namespace Chapter5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
