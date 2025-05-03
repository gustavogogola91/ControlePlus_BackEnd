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
                var estoques = await _database.tb_estoque.Include(e => e.Produto).ThenInclude(e => e.Fornecedor)
                    .Include(p => p.Produto).ThenInclude(p => p.Setor).ToListAsync();

                if (estoques == null || !estoques.Any())
                {
                    return NotFound("Não existem estoques cadastrados.");
                }

                var estoquesDTO = _mapper.Map<List<EstoqueDTO>>(estoques);
                return Ok(estoquesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EstoqueDTO>> GetEstoqueById(int id)
        {
            try
            {
                var estoque = await _database.tb_estoque.Include(e => e.Produto).ThenInclude(e => e.Fornecedor)
                    .Include(p => p.Produto).ThenInclude(p => p.Setor).FirstOrDefaultAsync(e => e.Id == id);

                if (estoque != null)
                {
                    var estoqueDTO = _mapper.Map<EstoqueDTO>(estoque);
                    return Ok(estoqueDTO);
                }

                return NotFound($"Estoque id {id} não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("produto/{id}")]
        public async Task<ActionResult<EstoqueDTO>> GetEstoqueByProdutoId(int id)
        {
            try
            {
                var estoque = await _database.tb_estoque.Include(e => e.Produto).ThenInclude(e => e.Fornecedor)
                    .Include(p => p.Produto).ThenInclude(p => p.Setor).FirstOrDefaultAsync(e => e.ProdutoId == id);

                if (estoque != null)
                {
                    var estoqueDTO = _mapper.Map<EstoqueDTO>(estoque);
                    return Ok(estoqueDTO);
                }

                return NotFound("Estoque relacionado ao produto id {id} não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> NewEstoque([FromBody] EstoquePostDTO estoquePostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validacao = await _database.tb_estoque.FirstOrDefaultAsync(e => e.ProdutoId == estoquePostDto.ProdutoId);

                if (validacao == null)
                {
                    var estoque = _mapper.Map<Estoque>(estoquePostDto);

                    _database.tb_estoque.Add(estoque);
                    await _database.SaveChangesAsync();

                    var estoqueDto = _mapper.Map<EstoqueDTO>(estoque);
                    return Created("Criado com sucesso", estoqueDto);
                }

                var produto = await _database.tb_produto.FindAsync(estoquePostDto.ProdutoId);
                return BadRequest($"Já existe um estoque associado ao produto {produto.Nome}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<EstoqueDTO>> ModifyEstoque([FromBody] EstoquePutDTO estoquePutDTO, int id)
        {
            try
            {
                var estoque = await _database.tb_estoque.FirstOrDefaultAsync(e => e.Id == id);

                if (estoque != null)
                {
                    if(estoquePutDTO.Quantidade != 0) {
                        estoque.Quantidade = estoquePutDTO.Quantidade;
                    }
                    if(estoquePutDTO.QuantidadeAlerta != 0) {
                        estoque.QuantidadeAlerta = estoquePutDTO.QuantidadeAlerta;
                    }
                    if(estoquePutDTO.QuantidadeVendidos != 0) {
                        estoque.QuantidadeVendidos = estoquePutDTO.QuantidadeVendidos;
                    }

                    _database.tb_estoque.Update(estoque);
                    await _database.SaveChangesAsync();

                    var estoqueDTO = _mapper.Map<EstoqueDTO>(estoque);
                    return Ok(estoqueDTO);
                }

                return NotFound($"Estoque id {id} não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
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
                    _database.tb_estoque.Remove(estoque);
                    await _database.SaveChangesAsync();

                    return NoContent();
                }

                return NotFound($"Estoque id {id} não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}