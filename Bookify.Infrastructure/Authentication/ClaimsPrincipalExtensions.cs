using System.Security.Claims;
using System;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Bookify.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        return Guid.TryParse(userId, out var parsedUserId) 
            ? parsedUserId 
            : throw new InvalidOperationException("User identifier is unavailable");
    }

    public static string GetIdentityId(this ClaimsPrincipal principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
               throw new InvalidOperationException("User identity is unavailable");
    }
}
