using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Dto;
using Microsoft.IdentityModel.Tokens;

namespace Backend.services
{
    public class JwtService : IJwtService
    {

        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJWT(JwtDTO jwt)
        {
            var authClaims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Nome!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, jwt.Roles.ToString()!),
                };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpirationMinutes"]!)),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)), SecurityAlgorithms.HmacSha256)
            );

            string Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }
    }
}