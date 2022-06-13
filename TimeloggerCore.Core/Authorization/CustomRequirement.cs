using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeloggerCore.Core.Authorization
{
    //public class CustomRequireClaim : IAuthorizationRequirement
    //{
    //    public CustomRequireClaim(string claimType)
    //    {
    //        ClaimType = claimType;
    //    }

    //    public string ClaimType { get; }
    //}

    //public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
    //{
    //    protected override Task HandleRequirementAsync(
    //        AuthorizationHandlerContext context,
    //        CustomRequireClaim requirement)
    //    {
    //        var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);
    //        if (hasClaim)
    //        {
    //            context.Succeed(requirement);
    //        }

    //        return Task.CompletedTask;
    //    }
    //}

    //public static class AuthorizationPolicyBuilderExtensions
    //{
    //    public static AuthorizationPolicyBuilder RequireCustomClaim(
    //        this AuthorizationPolicyBuilder builder,
    //        string claimType)
    //    {
    //        builder.AddRequirements(new CustomRequireClaim(claimType));
    //        return builder;
    //    }
    //}

    
    public class CustomRequirement : AuthorizationHandler<CustomRequirement>, IAuthorizationRequirement
    {
        public string ClaimType { get; }
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<CustomRequirement> logger;

        public CustomRequirement(
            string claimType,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor, 
            ILogger<CustomRequirement> logger)
        {
            ClaimType = claimType;
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);
            if (hasClaim)
            {
                context.Succeed(requirement);
            }
            if (context.User.Identity.IsAuthenticated)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(
            this AuthorizationPolicyBuilder builder,
            string claimType,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CustomRequirement> logger)
        {
            builder.AddRequirements(new CustomRequirement(claimType, configuration, httpContextAccessor, logger));
            return builder;
        }
    }
}
