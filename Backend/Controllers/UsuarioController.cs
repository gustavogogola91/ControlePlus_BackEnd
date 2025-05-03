using AutoMapper;
using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//TODO: Implementar um requisição simples e uma detalhada
//TODO: Implementar DTOs nos metodos GET, PUT e POST

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _database;

        public UsuarioController(IMapper mapper, AppDbContext database)
        {
            _mapper = mapper;
            _database = database;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAll()
        {
            try
            {
                var usuarios = await _database.tb_usuario.ToListAsync();

                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound("Não existem usuários cadastrados.");
                }

                var usuariosDTO = _mapper.Map<List<UsuarioDTO>>(usuarios);
                return Ok(usuariosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("detalhado")]
        public async Task<ActionResult<IEnumerable<UsuarioDetalhadoDTO>>> GetAllDetailed()
        {
            try
            {
                var usuarios = await _database.tb_usuario.ToListAsync();

                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound("Não existem usuários cadastrados.");
                }

                var usuariosDTO = _mapper.Map<List<UsuarioDetalhadoDTO>>(usuarios);
                return Ok(usuariosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Id == id);

                if (usuario == null)
                {
                    return NotFound($"Usuário id {id} não está cadastrado.");
                }

                var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("/username/{username}")]
        public async Task<ActionResult<UsuarioDetalhadoDTO>> GetUsuarioByUsername(string username)
        {
            try
            {
                var usuario = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Username == username);

                if (usuario == null)
                {
                    return NotFound($"Usuário username {username} não está cadastrado.");
                }

                var usuarioDTO = _mapper.Map<UsuarioDetalhadoDTO>(usuario);
                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}/detalhado")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioDetailedById(int id)
        {
            try
            {
                var usuario = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Id == id);

                if (usuario == null)
                {
                    return NotFound($"Usuário id {id} não está cadastrado.");
                }

                var usuarioDTO = _mapper.Map<UsuarioDetalhadoDTO>(usuario);
                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> NewUsuario([FromBody] UsuarioPostDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validacao = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Username == usuarioDTO.Username);
                if (validacao != null)
                {
                    return BadRequest($"Já existe um usuario com o Username {usuarioDTO.Username}");
                }

                var usuario = _mapper.Map<Usuario>(usuarioDTO);

                usuario.DataCriacao = DateTime.UtcNow;
                usuario.DataUltimaAtualizacao = DateTime.UtcNow;
                usuario.Ativo = true;
                _database.tb_usuario.Add(usuario);
                await _database.SaveChangesAsync();
                return Created("Criado com sucesso", usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //TODO: implementar essa bomba
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyUser(int id, [FromBody] UsuarioUpdateDTO usuarioMod)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuarioExistente = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Id == id);

                if (usuarioExistente == null)
                {
                    return NotFound($"Setor id {id} não encontrado");
                }

                _mapper.Map(usuarioMod, usuarioExistente);

                await _database.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {

            try
            {
                var usuario = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Id == id);

                if (usuario != null)
                {
                    _database.Remove(usuario);
                    await _database.SaveChangesAsync();
                    return Ok();
                }

                return NotFound($"Não foi possível encontrar o usuario com id {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}