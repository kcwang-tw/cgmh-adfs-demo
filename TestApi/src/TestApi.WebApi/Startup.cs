using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TestApi.WebApi.Startup))]

namespace TestApi.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Note : Need install Microsoft.Owin.Host.SystemWeb

            ConfigureAuth(app);
        }
    }
}
