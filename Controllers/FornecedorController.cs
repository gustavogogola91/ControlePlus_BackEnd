using AutoMapper;
using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("fornecedor")]
    public class FornecedorController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _database;

        public FornecedorController(IMapper mapper, AppDbContext database)
        {
            _mapper = mapper;
            _database = database;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> GetFornecedor()
        {
            try
            {
                var fornecedores = await _database.tb_fornecedor.Include(f => f.Produtos)
                    .ThenInclude(p => p.Setor).ToListAsync();

                if (fornecedores == null || !fornecedores.Any())
                {
                    return NotFound("Não existem Fornecedores cadastrados");
                }

                var fornecedoresDTO = _mapper.Map<List<FornecedorDTO>>(fornecedores);
                return Ok(fornecedoresDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FornecedorDTO>> GetFornecedorById(int id)
        {
            try
            {
                var fornecedor = await _database.tb_fornecedor.Include(f => f.Produtos)
                    .ThenInclude(p => p.Setor).FirstOrDefaultAsync(f => f.Id == id);

                if (fornecedor != null)
                {
                    var fornecedorDTO = _mapper.Map<FornecedorDTO>(fornecedor);
                    return Ok(fornecedorDTO);
                }

                return NotFound($"Fornecedor id {id} não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("nome/{name}")]
        public async Task<ActionResult<FornecedorDTO>> GetFornecedorByName(string name)
        {
            try
            {
                var fornecedor = await _database.tb_fornecedor.Include(f => f.Produtos)
                    .ThenInclude(p => p.Setor).FirstOrDefaultAsync(f => f.Nome == name);

                if (fornecedor != null)
                {
                    var fornecedorDTO = _mapper.Map<FornecedorDTO>(fornecedor);
                    return Ok(fornecedorDTO);
                }

                return NotFound($"Fornecedor nome {name} não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> NewFornecedor([FromBody] FornecedorPostDTO fornecedorDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validation = await _database.tb_fornecedor.FirstOrDefaultAsync(f => f.Nome == fornecedorDTO.Nome);

                if (validation == null)
                {
                    var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);

                    _database.tb_fornecedor.Add(fornecedor);
                    await _database.SaveChangesAsync();

                    var fornecedorReturn = _mapper.Map<FornecedorDTO>(fornecedor);
                    return CreatedAtAction(nameof(GetFornecedorByName), new { nome = fornecedorReturn.Nome }, fornecedorReturn);
                }

                return BadRequest($"Fornecedor {validation.Nome} já existe no banco de dados.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyFornecedor([FromBody] FornecedorPostDTO fornecedorModificado, int id)
        {
            try
            {
                var fornecedor = await _database.tb_fornecedor.FindAsync(id);

                if (fornecedor != null)
                {
                    if (fornecedorModificado.Nome != null)
                    {
                        fornecedor.Nome = fornecedorModificado.Nome;
                    }
                    if (fornecedorModificado.Endereco != null)
                    {
                        fornecedor.Endereco = fornecedorModificado.Endereco;
                    }
                    if (fornecedorModificado.Contato != null)
                    {
                        fornecedor.Contato = fornecedorModificado.Contato;
                    }

                    _database.tb_fornecedor.Update(fornecedor);
                    await _database.SaveChangesAsync();

                    var fornecedorDTO = _mapper.Map<FornecedorDTO>(fornecedor);
                    return Ok(fornecedorDTO);
                }

                return NotFound($"Fornecedor id {id} não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedor([FromBody] Fornecedor fornecedorModificado, int id)
        {
            try
            {
                var fornecedor = await _database.tb_fornecedor.FindAsync();

                if (fornecedor != null)
                {
                    _database.tb_fornecedor.Remove(fornecedor);
                    await _database.SaveChangesAsync();
                    return NoContent();
                }

                return NotFound($"Fornecedor id {id} não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}