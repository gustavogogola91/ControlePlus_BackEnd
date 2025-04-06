using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("setor")]
    public class SetorController : ControllerBase
    {

        private readonly AppDbContext _database;

        public SetorController(AppDbContext database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setor>>> GetAll()
        {
            try
            {
                var setores = await _database.tb_setor.ToListAsync();

                return Ok(setores);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar Setores");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Setor>> GetSetorById(int id)
        {
            try
            {
                var setor = await _database.tb_setor.FirstOrDefaultAsync(s => s.Id == id);

                if (setor != null)
                {
                    return Ok(setor);
                }

                return NotFound($"Setor id {id} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar dados");
            }
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<Setor>> GetSetorByName(string nome)
        {
            try
            {
                var setor = await _database.tb_setor.FirstOrDefaultAsync(s => s.Nome == nome);

                if (setor != null)
                {
                    return Ok(setor);
                }

                return NotFound($"Setor {nome} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar dados");
            }
        }

        [HttpPost]
        public async Task<IActionResult> NewSetor([FromBody] Setor setor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var validacao = await _database.tb_setor.FirstOrDefaultAsync(s => s.Nome == setor.Nome);

                if (validacao == null)
                {
                    _database.tb_setor.Add(setor);
                    await _database.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetSetorByName), new { nome = setor.Nome }, setor);
                }
                return BadRequest($"Já existe um setor com o nome {setor.Nome}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao adicionar novo setor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifySetor(int id, [FromBody] Setor setorModificado)
        {

            try
            {
                var setor = await _database.tb_setor.FirstOrDefaultAsync(s => s.Id == id);

                if (setor != null)
                {
                    if (!string.IsNullOrEmpty(setorModificado.Nome))
                    {
                        setor.Nome = setorModificado.Nome;
                    }

                    if (setorModificado.UsuarioId == 0)
                    {
                        setor.UsuarioId = setorModificado.UsuarioId;
                    }

                    _database.tb_setor.Update(setor);
                    await _database.SaveChangesAsync();
                    return NoContent();
                }
                return NotFound($"Setor id {id} não encontrado");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao modificar o setor");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetor(int id)
        {

            try
            {
                var setor = await _database.tb_setor.FirstOrDefaultAsync(s => s.Id == id);

                if (setor != null)
                {
                    _database.Remove(setor);
                    await _database.SaveChangesAsync();
                    return Ok();
                }

                return NotFound($"Não fpo possível encontrar o setor com id {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao deletar setor");
            }
        }


    }
}