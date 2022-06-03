using TimeloggerCore.Core.Security;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Core.Models
{
    public class CurrentUser
    {
        private UserClaims ClaimsContext;
        private ClaimsPrincipal Principal;
        public CurrentUser(IHttpContextAccessor context)
        {
            ClaimsContext = new UserClaims();

            var claims = context.HttpContext.User.Claims.ToList();
            ClaimsContext.Id = claims.GetClaimValue(TimeloggerCoreClaims.Id);
            ClaimsContext.FirstName = claims.GetClaimValue(TimeloggerCoreClaims.FirstName);
            ClaimsContext.LastName = claims.GetClaimValue(TimeloggerCoreClaims.LastName);
            ClaimsContext.UserName = claims.GetClaimValue(TimeloggerCoreClaims.Name);
            ClaimsContext.Email = claims.GetClaimValue(TimeloggerCoreClaims.Email);
            ClaimsContext.Roles = claims.GetClaimValues(TimeloggerCoreClaims.Role).ToList();
        }

        public UserClaims Claims
        {
            get
            {
                return ClaimsContext;
            }
        }
    }
}
