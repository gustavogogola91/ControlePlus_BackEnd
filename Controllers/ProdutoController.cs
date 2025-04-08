using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _database;

        public ProdutoController(AppDbContext database)
        {
            _database = database;
        }        

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        {
            try
            {
                var produtos = await _database.tb_produto.ToListAsync();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar produto");
            }
        }

        [HttpGet("{Cod}")]
        public async Task<ActionResult<Produto>> GetProdutoById(int Cod)
        {
            try
            {
                var produto = await _database.tb_produto.FirstOrDefaultAsync(p => p.Cod == Cod);
                if (produto != null){
                    return Ok(produto);
                }

                return NotFound($"Produto Cod {Cod} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar dados");
            }
        }

        [HttpPost]
        public async Task<IActionResult> NewProduto([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validacao = await _database.tb_produto.FirstOrDefaultAsync(p => p.Cod == produto.Cod);
                if (validacao == null)
                {
                    _database.tb_produto.Add(produto);
                    await _database.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest($"Já existe um produto com o Id {produto.Cod}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao adicionar novo produto");
            }
        }


    }



}