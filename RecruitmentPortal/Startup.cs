using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecruitmentPortal.Startup))]
namespace RecruitmentPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
