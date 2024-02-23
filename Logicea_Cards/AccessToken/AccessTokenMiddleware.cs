using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Logicea_Cards.AccessToken
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;
        IConfiguration _iConfig;

        public AccessTokenMiddleware(RequestDelegate next, IConfiguration iConfig)
        {
            _next = next;
            _iConfig = iConfig;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var principal = ValidateToken(token);

                if (principal != null)
                {
                    context.User = principal;
                }
            }

            await _next(context);
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, 
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfig.GetSection("MySettings").GetSection("AppSecretKey").Value)),
                ValidateLifetime = true 
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
