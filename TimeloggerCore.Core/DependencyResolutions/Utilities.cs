using AutoMapper;
using TimeloggerCore.Core.Authorization;
using TimeloggerCore.Core.Communication;
using TimeloggerCore.Data.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Filters;
using TimeloggerCore.Common.Helpers;
using TimeloggerCore.Common.Helpers.Interfaces;

namespace TimeloggerCore.Core.DependencyResolutions
{
    public static class Utilities
    {
        public static void RegisterLifetime(IServiceCollection services)
        {
            // Todo: we have to change to Interfaces in some of following:
            Communications.RegisterServices(services);
            //var config = ModelMapper.Configure();
            //IMapper mapper = config.CreateMapper();
            //services.AddSingleton(mapper);
            //services.AddAutoMapper(typeof(Startup));
            
            services.AddAutoMapper();
            services.AddTransient<IHttpClient, HttpClientHelper>();

            services.AddScoped<ValidateModelState>();

            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
        }

        public static void LifetimeApps(IApplicationBuilder apps)
        {

        }
    }
}
