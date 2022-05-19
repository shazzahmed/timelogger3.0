﻿using TimeloggerCore.Services;
using TimeloggerCore.Services.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeloggerCore.Services.Services;

namespace TimeloggerCore.Services.ServicesDependencyResolutions
{
    public static class ServiceModule
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IStatusTypeService, StatusTypeService>();

            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationTemplateService, NotificationTemplateService>();
            services.AddScoped<INotificationTypeService, NotificationTypeService>();
        }
    }
}
