using ControlePlus_BackEnd.db;
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
        private readonly AppDbContext _database;

        public UsuarioController(AppDbContext database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            try
            {
                var usuarios = await _database.tb_usuario.ToListAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar usuários");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Id == id);
                if (usuario != null){
                    return Ok(usuario);
                }

                return NotFound($"Usuario id {id} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar dados");
            }
        }
        

        [HttpPost]
        public async Task<IActionResult> NewUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validacao = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Id == usuario.Id);
                if (validacao == null)
                {

                    usuario.DataCriacao = DateTime.UtcNow;
                    usuario.DataUltimaAtualizacao = DateTime.UtcNow;
                    _database.tb_usuario.Add(usuario);
                    await _database.SaveChangesAsync();
                    // return CreatedAtAction(nameof(GetUsuarioById), new { nome = usuario.Nome }, usuario );
                    return NoContent();
                }
                return BadRequest($"Já existe um usuario com o Id {usuario.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao adicionar novo usuario");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyUser(int id, [FromBody] Usuario usuarioMod)
        {
            try
            {
                var usuarioExistente = await _database.tb_usuario.FindAsync(id);
                if (usuarioExistente != null)
                {
                    usuarioExistente.Nome = usuarioMod.Nome;
                    usuarioExistente.Username = usuarioMod.Username;
                    usuarioExistente.SetorId = usuarioMod.SetorId;
                    usuarioExistente.Roles = usuarioMod.Roles;
                    usuarioExistente.Ativo = usuarioMod.Ativo;
                    usuarioExistente.DataUltimaAtualizacao = DateTime.UtcNow;

                    await _database.SaveChangesAsync();
                    return Ok(usuarioExistente);
                }
                return NotFound($"Setor id {id} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao modificar o Usuario");
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

                return NotFound($"Não fpo possível encontrar o usuario com id {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao deletar usuario");
            }
        }


    }

}