using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeloggerCore.Common.Helpers;
using TimeloggerCore.Common.Options;
using static TimeloggerCore.Common.Utility.Enums;

namespace TimeloggerCore.Core.DependencyResolutions
{
    public static class ConfigurationModule
    {

        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration, ApplicationType applicationType)
        {
            services.Configure<TimeloggerCoreOptions>(configuration.GetSection("TimeloggerCoreOptions"));
            services.Configure<ComponentOptions>(configuration.GetSection("Component"));
            services.Configure<InfrastructureOptions>(configuration.GetSection("Infrastructure"));
            services.Configure<SecurityOptions>(configuration.GetSection("Security"));
            services.Configure<GoogleOptions>(configuration.GetSection("Google"));
            services.Configure<OutlookOptions>(configuration.GetSection("Outlook"));
            services.Configure<FacebookOptions>(configuration.GetSection("Facebook"));
            services.Configure<TwitterOptions>(configuration.GetSection("Twitter"));
            AppServicesHelper.Configuration = configuration;
            return services;
        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppServicesHelper.Services = app.ApplicationServices;
        }
    }
}
