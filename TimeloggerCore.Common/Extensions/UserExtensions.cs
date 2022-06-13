using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace TimeloggerCore.Common.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Retrieve a Claims by name and indicates if it succeeded.
        /// </summary>
        /// <param name="user">The current User.</param>
        /// <param name="claimsName">Queried claims name.</param>
        /// <param name="claimsValue">The value of the claims.</param>
        /// <returns>True if the Claims exist with a value.</returns>
        public static bool TryGetUserClaims(this ClaimsPrincipal user, string claimsName, out string claimsValue)
        {
            claimsValue = user?.Claims?.FirstOrDefault(c => c.Type == claimsName)?.Value;

            return !string.IsNullOrWhiteSpace(claimsValue);
        }
        /// <summary>
        /// Get user id from the claims principal.
        /// </summary>
        /// <param name="user">Claims pricipal representing the user.</param>
        /// <param name="userId">User Id obtained from the claims principal.</param>
        /// <returns>True if it could find the user id.</returns>
        public static bool TryGetUserId(this ClaimsPrincipal user, out Guid userId)
        {
            // First, try to identify the user by player id
            if (user.TryGetUserClaims(JwtClaimTypes.Id, out var playerIdString)
                && Guid.TryParse(playerIdString, out userId))
            {
                return true;
            }

            userId = Guid.Empty;
            return false;
        }
        public static bool TryGetAccessToken(this ClaimsPrincipal user, out string accessToken)
        {
            return user.TryGetUserClaims("access_token", out accessToken);
        }
        public static bool TryGetIdToken(this ClaimsPrincipal user, out string idToken)
        {
            return user.TryGetUserClaims("id_token", out idToken);
        }
        public static bool TryGetExternalId(this ClaimsPrincipal user, out string externalId)
        {
            return user.TryGetUserClaims(ClaimTypes.NameIdentifier, out externalId);
        }
        public static string GetAccessToken(this ClaimsPrincipal principal)
        {
            return principal.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;
        }

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.Claims
                            .FirstOrDefault(c => c.Type == JwtClaimTypes.Id)?.Value;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            return principal.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public static string GetUserFullName(this ClaimsPrincipal principal)
        {
            var firstName = principal.Claims
                                     .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;

            var lastName = principal.Claims
                                    .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

            return $"{firstName} {lastName}";
        }

        public static string GetUserFistName(this ClaimsPrincipal principal)
        {
            return principal.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
        }

        public static string GetUserLastName(this ClaimsPrincipal principal)
        {
            return principal.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
        }
        public static string GetUserRole(this ClaimsPrincipal principal)
        {
            return principal.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        }

        public static bool UserIsInRole(this ClaimsPrincipal principal, string role)
        {
            var actualRole = principal.Claims
                                      .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            return actualRole == role;
        }
    }
}
