using System;
using System.Threading.Tasks;
using Glimpse.Mvc.AlternateType;
using Microsoft.Owin;
using Owin;
//using Hangfire;
//using Hangfire.SqlServer;
//using Hangfire.Dashboard;

[assembly: OwinStartup(typeof(PolTrain.Startup))]

namespace PolTrain
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

//            app.UseHangfire(config =>
//            {
//                config.UseAuthorizationFilters(new AuthorizationFilter
//                {
//                    Roles = "Admin"
//                });

//                config.UseSqlServerStorage("StoreContext");
//                config.UseServer();
//            });
        }
    }
}
