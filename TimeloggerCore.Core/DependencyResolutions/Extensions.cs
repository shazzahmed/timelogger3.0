using TimeloggerCore.Core.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Core.DependencyResolutions
{
    public static class Extensions
    {
        public static void Configure(this IServiceCollection services, IConfiguration configuration, ApplicationType applicationType)
        {
            InfrastructureModule.Configure(services, configuration, applicationType);
            Middleware.RegisterServices(services, configuration, applicationType);
            Securities.RegisterServices(services, configuration, applicationType);
        }


        public static void RegisterApps(this IApplicationBuilder app, IWebHostEnvironment env, ApplicationType applicationType)
        {
            InfrastructureModule.Configure(app, env);
            Middleware.RegisterApps(app, env, applicationType);
        }
    }
}
