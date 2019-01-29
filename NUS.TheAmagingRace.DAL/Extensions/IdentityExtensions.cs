using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.DAL.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserDisplayName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("DisplayName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetImagePath(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ImagePath");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetisAssigned(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Assigned");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }

}
