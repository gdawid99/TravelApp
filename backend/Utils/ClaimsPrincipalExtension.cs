using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TravelApp.Utils
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            return Guid.TryParse(value, out var id) ? id : Guid.Empty;
        }
    }
}
