using AutoMapper;
using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("movimentacao")]
    public class MovimentacaoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _database;

        public MovimentacaoController(IMapper mapper, AppDbContext database)
        {
            _mapper = mapper;
            _database = database;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimentacaoDTO>>> GetAll()
        {
            try
            {
                var movimentacoes = await _database.tb_movimentacao.Include(m => m.Usuario).Include(m => m.Produto).ThenInclude(p => p.Setor)
                    .Include(m => m.Produto).ThenInclude(p => p.Fornecedor).ToListAsync();

                if (movimentacoes == null || !movimentacoes.Any())
                {
                    return NotFound("Não existem movimentações cadastradas.");
                }

                var movimentacoesDTO = _mapper.Map<List<MovimentacaoDTO>>(movimentacoes);
                return Ok(movimentacoesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MovimentacaoDTO>> GetMovimentacaoById(int id)
        {
            try
            {
                var movimentacao = await _database.tb_movimentacao.Include(m => m.Usuario).Include(m => m.Produto).ThenInclude(p => p.Setor)
                    .Include(m => m.Produto).ThenInclude(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);

                if (movimentacao != null)
                {
                    var movimentacaoDTO = _mapper.Map<MovimentacaoDTO>(movimentacao);
                    return Ok(movimentacaoDTO);
                }

                return NotFound($"Movimentação com id {id} não encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("produto/{id}")]
        public async Task<ActionResult<IEnumerable<MovimentacaoDTO>>> GetMovimentacaoByProdutoId(int id)
        {
            try
            {
                var movimentacoes = await _database.tb_movimentacao.Include(m => m.Usuario).Include(m => m.Produto).ThenInclude(p => p.Setor)
                    .Include(m => m.Produto).ThenInclude(p => p.Fornecedor).Where(m => m.ProdutoId == id).ToListAsync();

                if (movimentacoes == null || !movimentacoes.Any())
                {
                    return NotFound($"Não existem movimentações ligadas ao produto id {id}");
                }

                var movimentacoesDTO = _mapper.Map<List<MovimentacaoDTO>>(movimentacoes);
                return Ok(movimentacoesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("usuario/{id}")]
        public async Task<ActionResult<IEnumerable<MovimentacaoDTO>>> GetMovimentacaoByUsuarioId(int id)
        {
            try
            {
                var movimentacoes = await _database.tb_movimentacao.Include(m => m.Usuario).Include(m => m.Produto).ThenInclude(p => p.Setor)
                    .Include(m => m.Produto).ThenInclude(p => p.Fornecedor).Where(m => m.UsuarioId == id).ToListAsync();

                if (movimentacoes == null || !movimentacoes.Any())
                {
                    return NotFound($"Não existem movimentações ligadas ao usuario id {id}");
                }

                var movimentacoesDTO = _mapper.Map<List<MovimentacaoDTO>>(movimentacoes);
                return Ok(movimentacoesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult<MovimentacaoDTO>> NewMovimentacao([FromBody] MovimentacaoPostDTO movimentacaoPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var movimentacao = _mapper.Map<Movimentacao>(movimentacaoPostDTO);

                _database.tb_movimentacao.Add(movimentacao);
                await _database.SaveChangesAsync();

                var movimentacaoDTO = _mapper.Map<MovimentacaoDTO>(movimentacao);
                return CreatedAtAction(nameof(GetMovimentacaoById), new { id = movimentacao.Id }, movimentacaoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MovimentacaoDTO>> ModifyMovimentacao(int id, [FromBody] MovimentacaoPutDTO movimentacaoPutDTO)
        {
            try
            {
                var movimentacao = await _database.tb_movimentacao.FirstOrDefaultAsync(m => m.Id == id);

                if (movimentacao != null)
                {

                    if (movimentacaoPutDTO.ProdutoId != 0)
                    {
                        movimentacao.ProdutoId = movimentacaoPutDTO.ProdutoId;
                    }
                    if (movimentacaoPutDTO.Quantidade != 0)
                    {
                        movimentacao.Quantidade = movimentacaoPutDTO.Quantidade;
                    }
                    // TODO: Verificação de null está impedindo a alteração dos Tipo
                    if (movimentacaoPutDTO.Tipo == null)
                    {
                        movimentacao.Tipo = movimentacaoPutDTO.Tipo;
                    }
                    if (movimentacaoPutDTO.Observacao != null)
                    {
                        movimentacao.Observacao = movimentacaoPutDTO.Observacao;
                    }

                    _database.Update(movimentacao);
                    await _database.SaveChangesAsync();

                    var movimentacaoDTO = _mapper.Map<MovimentacaoDTO>(movimentacao);
                    return Ok(movimentacaoDTO);
                }

                return NotFound($"Movimentação id {id} não encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimentacao(int id)
        {
            try
            {
                var movimentacao = await _database.tb_movimentacao.FirstOrDefaultAsync(m => m.Id == id);

                if (movimentacao != null)
                {
                    _database.tb_movimentacao.Remove(movimentacao);
                    await _database.SaveChangesAsync();

                    return NoContent();
                }

                return NotFound($"Movimentação id {id} não encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}