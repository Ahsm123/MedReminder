using MedReminder.Web.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedReminder.Web.Service
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetJwtToken()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["JwtToken"];
        }

        public void SetJwtToken(string token, TimeSpan expirationTime)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.Add(expirationTime)
            });
        }

        public void DeleteJwtToken()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("JwtToken");
        }

        public int? ExstractUserIdFromToken()
        {
            var token = GetJwtToken();
            if (string.IsNullOrEmpty(token)) return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
        }


    }
}
