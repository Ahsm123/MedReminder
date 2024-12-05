using System.IdentityModel.Tokens.Jwt;

namespace MedReminder.Web.Helpers;

public static class JwtTokenHelper
{
    public static bool IsTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null) return true;

        var expiration = jwtToken.ValidTo;
        return expiration < DateTime.Now;
    }
}
