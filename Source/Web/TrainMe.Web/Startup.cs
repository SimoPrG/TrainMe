using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(TrainMe.Web.Startup))]

namespace TrainMe.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
