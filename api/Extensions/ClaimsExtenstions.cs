using System.Security.Claims;
using Microsoft.Extensions.ObjectPool;

namespace api.Extensions;
public static class ClaimsExtenstions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        // var kme = user.Claims;
        var kme = user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"));
        return kme?.Value;
    }
}
