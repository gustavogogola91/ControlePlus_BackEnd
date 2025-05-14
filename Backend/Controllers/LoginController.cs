using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
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
        private readonly IEncryptService _hasher;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwt;

        public LoginController(AppDbContext database, IEncryptService hasher, IMapper mapper, IJwtService jwt)
        {
            _database = database;
            _hasher = hasher;
            _mapper = mapper;
            _jwt = jwt;
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
                var userJwt = _mapper.Map<JwtDTO>(user);
                var token = _jwt.GenerateJWT(userJwt);

                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}