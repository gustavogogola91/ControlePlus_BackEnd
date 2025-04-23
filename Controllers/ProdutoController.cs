using AutoMapper;
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

        private readonly IMapper _mapper;
        private readonly AppDbContext _database;

        public ProdutoController(IMapper mapper, AppDbContext database)
        {
            _mapper = mapper;
            _database = database;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        {
            try
            {
                var produtos = await _database.tb_produto.ToListAsync();

                if (produtos == null || !produtos.Any())
                {
                    return NotFound("Não existem produtos cadastrados.");
                }

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{Cod}")]
        public async Task<ActionResult<Produto>> GetProdutoById(int Cod)
        {
            try
            {
                var produto = await _database.tb_produto.FirstOrDefaultAsync(p => p.Cod == Cod);
                if (produto != null)
                {
                    return Ok(produto);
                }

                return NotFound($"Produto Cod {Cod} não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
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
                    return CreatedAtAction(nameof(GetProdutoById), new { id = produto.Cod }, produto);
                }
                return BadRequest($"Já existe um produto com o Id {produto.Cod}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }



}