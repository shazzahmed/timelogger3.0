using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using TimeloggerCore.Data.Database;
using TimeloggerCore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Data.Repository;
using TimeloggerCore.Common.Options;

namespace TimeloggerCore.Core.DependencyResolutions
{
    public static class RepositoryModule
    {
        public static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                        msSqlServerOptions => msSqlServerOptions.MigrationsAssembly("TimeloggerCore.Data")));
            var componentOptions = services.BuildServiceProvider().GetService<Microsoft.Extensions.Options.IOptionsSnapshot<ComponentOptions>>();
            if (componentOptions.Value.Security.SecurityService == "AspnetIdentity")
            {
                services.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
                services.AddTransient<ISqlServerDbContext, SqlServerDbContext>();
            }

            services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();
            services.AddScoped<IUnitOfWork>(unitOfWork => new UnitOfWork(unitOfWork.GetService<ISqlServerDbContext>()));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IStatusTypeRepository, StatusTypeRepository>();

            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationTemplateRepository, NotificationTemplateRepository>();
            services.AddTransient<INotificationTypeRepository, NotificationTypeRepository>();
            services.AddTransient<IPreviousPasswordsRepository, PreviousPasswordsRepository>();

            services.AddTransient<IClientAgencyRepository, ClientAgencyRepository>();
            services.AddTransient<IClientWorkerRepository, ClientWorkerRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddTransient<IInvitationRepository, InvitationRepository>();
            services.AddTransient<IInvitationRequestRepository, InvitationRequestRepository>();
            services.AddTransient<IMeetingRepository, MeetingRepository>();
            services.AddTransient<IPackageRepository, PackageRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IPricingRepository, PricingRepository>();
            services.AddTransient<IProjectInvitationRepository, ProjectInvitationRepository>();

            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IProjectWorkersRepository, ProjectWorkersRepository>();
            services.AddTransient<ITimeLogRepository, TimeLogRepository>();
            services.AddTransient<ITimeZoneRepository, TimeZoneRepository>();
            services.AddTransient<IWorkSessionRepository, WorkSessionRepository>();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DbMigrator.Migrate(app);
            DataSeeder.Seed(app);
        }
    }
}
