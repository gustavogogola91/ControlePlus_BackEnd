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
        public IActionResult GetAll()
        {
            ICollection<Categoria> a = _database.tb_categoria.ToList();

            return Ok(a);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoriaById(int id)
        {
            Categoria? categoria = _database.tb_categoria.FirstOrDefault(c => c.Id == id);

            if (categoria != null)
            {
                return Ok(categoria);
            }
            else
            {
                return NotFound($"Categoria com id {id} não encontrada");
            }
        }

        [HttpGet("nome/{name}")]
        public IActionResult GetCategoriaById(string name)
        {
            Categoria? categoria = _database.tb_categoria.FirstOrDefault(c => c.Nome == name);

            if (categoria != null)
            {
                return Ok(categoria);
            }
            else
            {
                return NotFound($"Categoria {name} não encontrada");
            }
        }

        [HttpPost("nova")]
        public async Task<IActionResult> NewCategoria([FromBody] Categoria categoria)
        {

            var validacao = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Nome == categoria.Nome);

            if (validacao == null)
            {

                Categoria newCat = new Categoria(categoria.Nome);

                try
                {
                    await _database.tb_categoria.AddAsync(newCat);
                    await _database.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetAll), new { id = categoria.Id }, categoria);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return BadRequest("Erro ao criar categoria!");
                }



            }
            else
            {
                return BadRequest("Já existe uma categoria com este nome");
            }

        }
    }
}