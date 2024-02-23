using Logicea_Cards.AccessToken;
using Logicea_Cards.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Logicea_Cards.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CardsDbContext _cardsDbContext;
        private readonly ITokenService _tokenService;

        public AuthController(CardsDbContext userContext, ITokenService tokenService)
        {
            _cardsDbContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var user = _cardsDbContext.Users.FirstOrDefault(u =>
                (u.Email == loginModel.EmailAddress) && (u.Password == loginModel.Password));
            if (user is null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            _cardsDbContext.SaveChanges();

            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
