using cinemaServer.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cinemaServer.Services
{
    public class TokenService
    {
        private const int ExpirationMinutes = 60 * 8;
        private readonly ILogger<TokenService> _logger;
        private readonly IConfiguration _configuration;

        public TokenService(ILogger<TokenService> logger, [FromServices] IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string CreateToken(ApplicationUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.LogInformation("JWT Token created");

            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims, SigningCredentials credentials, DateTime expiration)
        {
            var validIssuer = _configuration["JwtTokenSettings:ValidIssuer"];
            var validAudience = _configuration["JwtTokenSettings:ValidAudience"];

            return new JwtSecurityToken(
                validIssuer,
                validAudience,
                claims,
                expires: expiration,
                signingCredentials: credentials
            );
        }

        private static List<Claim> CreateClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            return claims;
        }

        private SigningCredentials CreateSigningCredentials()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtTokenSettings:SymmetricSecurityKey"]!)
            );

            return new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
