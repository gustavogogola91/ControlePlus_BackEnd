using AutoMapper;
using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//TODO: implementar DTOs nos metodos, PUT e GETid
//TODO: Fazer uma requisição de setores simples
namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("setor")]
    public class SetorController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _database;

        public SetorController(IMapper mapper, AppDbContext database)
        {
            _mapper = mapper;
            _database = database;
        }


        [HttpGet]
        [Route("setores")]
        public async Task<ActionResult<IEnumerable<SetorDTO>>> GetAll()
        {
            try
            {
                var setores = await _database.tb_setor.Include(s => s.Usuarios).ToListAsync();

                if (setores == null || !setores.Any())
                {
                    return NotFound("Não existem setores cadastrados.");
                }

                var setoresDTO = _mapper.Map<List<SetorDTO>>(setores);
                return Ok(setoresDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //TODO: implementar quando tiver a ProdutoDto simples
        [HttpGet]
        [Route("setores/detalhados")]
        public async Task<ActionResult<IEnumerable<SetorDetalhadoDTO>>> GetAllDetailed()
        {
            try
            {
                var setores = await _database.tb_setor
                .Include(s => s.Usuarios)
                .Include(s => s.Produtos)
                .ToListAsync();

                if (setores == null || !setores.Any())
                {
                    return NotFound("Não existem setores cadastrados.");
                }

                var setoresDTO = _mapper.Map<List<SetorDetalhadoDTO>>(setores);

                return Ok(setoresDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //TODO: IMplementar DTO aqui
        [HttpGet("{id}")]
        public async Task<ActionResult<Setor>> GetSetorById(int id)
        {
            try
            {
                var setor = await _database.tb_setor.FirstOrDefaultAsync(s => s.Id == id);

                if (setor != null)
                {
                    return Ok(setor);
                }

                return NotFound($"Setor id {id} não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //TODO: IMplementar DTO aqui
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<Setor>> GetSetorByName(string nome)
        {
            try
            {
                var setor = await _database.tb_setor.FirstOrDefaultAsync(s => s.Nome == nome);

                if (setor != null)
                {
                    return Ok(setor);
                }

                return NotFound($"Setor {nome} não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> NewSetor([FromBody] SetorPostDTO setorPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validacao = await _database.tb_setor.FirstOrDefaultAsync(s => s.Nome == setorPostDTO.Nome);

                if (validacao == null)
                {
                    var setor = _mapper.Map<Setor>(setorPostDTO);

                    await _database.tb_setor.AddAsync(setor);
                    await _database.SaveChangesAsync();

                    var setorDTO = _mapper.Map<SetorDTO>(setor);
                    return CreatedAtAction(nameof(GetSetorByName), new { nome = setorDTO.Nome }, setor);
                }

                return BadRequest($"Já existe um setor com o nome {setorPostDTO.Nome}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ModifySetor(int id, [FromBody] SetorUpdateDTO setorModificado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //os erros message dos negocio
            }

            try
            {
                var setor = await _database.tb_setor.FirstOrDefaultAsync(s => s.Id == id);

                if (setor == null)
                {
                    return NotFound($"Setor id {id} não encontrado");
                }

                _mapper.Map(setorModificado, setor);

                await _database.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetor(int id)
        {

            try
            {
                var setor = await _database.tb_setor.FirstOrDefaultAsync(s => s.Id == id);

                if (setor != null)
                {
                    _database.Remove(setor);
                    await _database.SaveChangesAsync();
                    return Ok();
                }

                return NotFound($"Não fpo possível encontrar o setor com id {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


    }
}