using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobTest.Startup))]
namespace JobTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
