using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Dto;
using Backend.services;
using ControlePlus_BackEnd.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _database;
        private readonly IConfiguration _config;
        private readonly IEncryptService _hasher;

        public LoginController(AppDbContext database, IConfiguration config, IEncryptService hasher)
        {
            _database = database;
            _config = config;
            _hasher = hasher;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Username == login.Username);
            if (user != null && _hasher.VerificarSenha(login.Senha!, user.Senha))
            {
                var authClaims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Nome!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Roles.ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpirationMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)), SecurityAlgorithms.HmacSha256)
                );
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized();
        }
    }
}