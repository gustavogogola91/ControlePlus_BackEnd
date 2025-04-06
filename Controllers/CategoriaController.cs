using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _database;

        public CategoriaController(AppDbContext database)
        {
            _database = database;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        {
            try
            {
                var categorias = await _database.tb_categoria.ToListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar categorias");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoriaById(int id)
        {
            try
            {
                var categoria = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Id == id);

                if (categoria != null)
                {
                    return Ok(categoria);
                }
                return NotFound($"Categoria com id {id} não encontrada");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar categorias");
            }

        }


        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> GetCategoriaByName(string nome)
        {
            try
            {
                var categoria = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Nome == nome);

                if (categoria != null)
                {
                    return Ok(categoria);
                }
                return NotFound($"Categoria {nome} não encontrada");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar categorias");
            }


        }

        [HttpPost]
        public async Task<IActionResult> NewCategoria([FromBody] Categoria categoria)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var validacao = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Nome == categoria.Nome);

                if (validacao == null)
                {
                    await _database.tb_categoria.AddAsync(categoria);
                    await _database.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetCategoriaByName), new { nome = categoria.Nome }, categoria);
                }
                return BadRequest("Já existe uma categoria com este nome");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Erro ao criar categoria");
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyCategoria(int id, [FromBody] Categoria categoriaModificada)
        {

            try
            {
                var categoria = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Id == id);

                if (categoria != null)
                {

                    var validacao = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Nome == categoriaModificada.Nome);

                    if (validacao == null)
                    {
                        categoria.Nome = categoriaModificada.Nome;
                        _database.tb_categoria.Update(categoria);
                        await _database.SaveChangesAsync();
                        return NoContent();

                    }
                    return BadRequest($"Já existe uma categoria com o nome {categoriaModificada.Nome}");

                }
                return NotFound($"Categoria id {id} não encontrada");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao alterar categoria");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {

                var categoria = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Id == id);

                if (categoria != null)
                {
                    _database.tb_categoria.Remove(categoria);
                    await _database.SaveChangesAsync();
                    return Ok();
                }

                return NotFound($"Categoria id {id} não encontrada");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao deletar categoria");
            }
        }
    }


}
