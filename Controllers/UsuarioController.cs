using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


    }

}