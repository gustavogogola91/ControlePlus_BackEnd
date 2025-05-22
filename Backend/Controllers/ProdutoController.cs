using AutoMapper;
using Backend.Dto;
using Backend.services;
using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Dto;
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
        private readonly IEstoqueService _estoqueService;

        public ProdutoController(IMapper mapper, AppDbContext database, IEstoqueService estoqueService)
        {
            _mapper = mapper;
            _database = database;
            _estoqueService = estoqueService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        {
            try
            {
                var produtos = await _database.tb_produto.Include(p => p.Categoria).Include(p => p.Fornecedor).Include(p => p.Setor).ToListAsync();

                if (produtos == null || !produtos.Any())
                {
                    return NotFound("Não existem produtos cadastrados.");
                }

                var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

                return Ok(produtosDTO);
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
                var produto = await _database.tb_produto.Include(p => p.Categoria).Include(p => p.Fornecedor).Include(p => p.Setor).FirstOrDefaultAsync(p => p.Cod == Cod);
                if (produto == null)
                {
                    return NotFound($"Produto Cod {Cod} não encontrado");
                }

                var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

                return Ok(produtoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> NewProduto([FromBody] ProdutoPostDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validacao = await _database.tb_produto.FirstOrDefaultAsync(p => p.Cod == dto.Cod);
                if (validacao == null)
                {
                    var produto = _mapper.Map<Produto>(dto);
                    _database.tb_produto.Add(produto);
                    await _estoqueService.CriarEstoque(produto.Cod!);
                    await _database.SaveChangesAsync();

                    var produtoCompleto = await _database.tb_produto
                        .Include(p => p.Setor)
                        .Include(p => p.Fornecedor)
                        .Include(p => p.Categoria)
                        .FirstOrDefaultAsync(p => p.Cod == produto.Cod);

                    var produtoDTO = _mapper.Map<ProdutoDTO>(produtoCompleto);
                    return Created("Criado com sucesso", produtoDTO);
                }
                return BadRequest($"Já existe um produto com o Id {dto.Cod}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{Cod}")]
        public async Task<IActionResult> ModifyProduto([FromBody] ProdutoPutDTO dto, int Cod)
        {
            try
            {
                var produto = await _database.tb_produto.FirstOrDefaultAsync(p => p.Cod == Cod);

                if (produto != null)
                {

                    if (dto.Nome != null)
                    {
                        produto.Nome = dto.Nome;
                    }
                    if (dto.Descricao != null)
                    {
                        produto.Descricao = dto.Descricao;
                    }
                    if (dto.SetorId != 0)
                    {
                        produto.SetorId = dto.SetorId;
                    }
                    if (dto.CategoriaId != 0)
                    {
                        produto.CategoriaId = dto.CategoriaId;
                    }
                    if (dto.FornecedorId != 0)
                    {
                        produto.FornecedorId = dto.FornecedorId;
                    }
                    if (dto.PrecoCompra > 0)
                    {
                        produto.PrecoCompra = dto.PrecoCompra;
                    }
                    if (dto.PrecoVenda > 0)
                    {
                        produto.PrecoVenda = dto.PrecoVenda;
                    }

                    _database.tb_produto.Update(produto);
                    await _database.SaveChangesAsync();

                    var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
                    return Ok(produtoDTO);
                }
                return NotFound($"Não foi possível encontrar o produto Cod {Cod}");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{Cod}")]
        public async Task<IActionResult> DeleteProduto(int Cod)
        {
            try
            {
                var produto = await _database.tb_produto.FirstOrDefaultAsync(p => p.Cod == Cod);

                if (produto == null)
                {
                    return NotFound($"Não foi possivel encontrar o produto Cod {Cod}");
                }

                _database.tb_produto.Remove(produto);
                await _database.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }



}