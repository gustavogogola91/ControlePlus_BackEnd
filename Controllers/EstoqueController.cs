using AutoMapper;
using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("estoque")]
    public class EstoqueController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _database;

        public EstoqueController(IMapper mapper, AppDbContext database)
        {
            _mapper = mapper;
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstoqueDTO>>> GetAll()
        {
            try
            {
                var estoques = await _database.tb_estoque.Include(e => e.Produto).ThenInclude(e => e.Fornecedor).Include(p => p.Produto).ThenInclude(p => p.Setor).ToListAsync();

                var estoquesDTO = _mapper.Map<List<EstoqueDTO>>(estoques);
                return Ok(estoquesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar o estoque");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstoqueDTO>> GetEstoqueById(int id)
        {
            try
            {
                var estoque = await _database.tb_estoque.Include(e => e.Produto).ThenInclude(e => e.Fornecedor).Include(p => p.Produto).ThenInclude(p => p.Setor).FirstOrDefaultAsync(e => e.Id == id);

                if (estoque != null)
                {
                    var estoqueDTO = _mapper.Map<List<EstoqueDTO>>(estoque);
                    return Ok(estoqueDTO);
                }
                return NotFound($"Estoque id {id} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar o estoque");
            }
        }

        [HttpGet("produto/{id}")]
        public async Task<ActionResult<Estoque>> GetByProdutoId(int id)
        {
            try
            {
                var estoque = await _database.tb_estoque.Include(e => e.Produto).ThenInclude(e => e.Fornecedor).Include(p => p.Produto).ThenInclude(p => p.Setor).FirstOrDefaultAsync(e => e.ProdutoId == id);

                if (estoque != null)
                {
                    var estoqueDTO = _mapper.Map<List<EstoqueDTO>>(estoque);
                    return Ok(estoqueDTO);
                }
                return NotFound("Estoque relacionado ao produto id {id} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar o estoque");
            }
        }

        [HttpPost]
        public async Task<IActionResult> NewEstoque([FromBody] Estoque estoque)
        {
            try
            {
                var validacao = await _database.tb_estoque.FirstOrDefaultAsync(e => e.ProdutoId == estoque.ProdutoId);

                if (validacao == null)
                {
                    _database.tb_estoque.Add(estoque);
                    await _database.SaveChangesAsync();
                    return Ok(estoque);
                }
                var produto = await _database.tb_produto.FindAsync(estoque.ProdutoId);
                return BadRequest($"Já existe um estoque associado ao produto {produto.Nome}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao adicionar estoque.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyEstoque([FromBody] Estoque estoqueModificado, int id)
        {
            try
            {
                var estoque = await _database.tb_estoque.FindAsync(id);

                if (estoque != null)
                {
                    _database.Entry(estoque).CurrentValues.SetValues(estoqueModificado);
                    await _database.SaveChangesAsync();
                }

                return NotFound($"Estoque id {id} não encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao alterar estoque.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstoque(int id)
        {
            try
            {
                var estoque = await _database.tb_estoque.FindAsync(id);

                if (estoque != null)
                {
                    _database.Remove(estoque);
                    await _database.SaveChangesAsync();
                    return NoContent();
                }
                return NotFound($"Estoque id {id} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao alterar estoque.");
            }
        }
    }
}