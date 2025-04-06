using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("estoque")]
    public class EstoqueController : ControllerBase
    {

        private readonly AppDbContext _database;

        public EstoqueController(AppDbContext database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var estoques = await _database.tb_estoque.ToListAsync();

                return Ok(estoques);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar o estoque");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estoque>> GetEstoqueById(int id)
        {
            try
            {
                var estoque = await _database.tb_estoque.FirstOrDefaultAsync(e => e.Id == id);

                if (estoque != null)
                {
                    return Ok(estoque);
                }
                return NotFound($"Estoque id {id} n'ao encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar o estoque");
            }
        }
    }
}