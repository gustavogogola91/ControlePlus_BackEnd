using AutoMapper;
using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("categoria")]
    public class CategoriaController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _database;

        public CategoriaController(IMapper mapper, AppDbContext database)
        {
            _mapper = mapper;
            _database = database;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAll()
        {
            try
            {
                var categorias = await _database.tb_categoria.Include(c => c.Produtos).ThenInclude(p => p.Fornecedor)
                    .Include(c => c.Produtos).ThenInclude(p => p.Setor).ToListAsync();

                if (categorias == null || !categorias.Any())
                {
                    return NotFound("Não existem categorias cadastradas.");
                }

                var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
                return Ok(categoriasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoriaById(int id)
        {
            try
            {
                var categoria = await _database.tb_categoria.Include(c => c.Produtos).ThenInclude(p => p.Setor)
                    .Include(c => c.Produtos).ThenInclude(p => p.Fornecedor).FirstOrDefaultAsync(c => c.Id == id);

                if (categoria != null)
                {
                    var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
                    return Ok(categoriaDto);
                }

                return NotFound($"Categoria com id {id} não encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoriaByName(string nome)
        {
            try
            {
                var categoria = await _database.tb_categoria.Include(c => c.Produtos).ThenInclude(p => p.Setor)
                    .Include(c => c.Produtos).ThenInclude(p => p.Fornecedor).FirstOrDefaultAsync(c => c.Nome == nome);

                if (categoria != null)
                {
                    var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
                    return Ok(categoriaDto);
                }

                return NotFound($"Categoria {nome} não encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> NewCategoria([FromBody] CategoriaPostDTO categoriaPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validacao = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Nome == categoriaPostDTO.Nome);

                if (validacao == null)
                {
                    var categoria = _mapper.Map<Categoria>(categoriaPostDTO);

                    _database.tb_categoria.Add(categoria);
                    await _database.SaveChangesAsync();

                    var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
                    return CreatedAtAction(nameof(GetCategoriaByName), new { nome = categoriaDTO.Nome }, categoriaDTO);
                }

                return BadRequest("Já existe uma categoria com este nome");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyCategoria(int id, [FromBody] CategoriaPostDTO categoriaPostDTO)
        {
            try
            {
                var categoria = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Id == id);

                if (categoria != null)
                {
                    var validacao = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Nome == categoriaPostDTO.Nome);

                    if (validacao == null && categoriaPostDTO.Nome != null)
                    {
                        categoria.Nome = categoriaPostDTO.Nome;
                        _database.tb_categoria.Update(categoria);
                        await _database.SaveChangesAsync();

                        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
                        return Ok(categoriaDTO);

                    }

                    return BadRequest($"Já existe uma categoria com o nome {categoriaPostDTO.Nome}");
                }

                return NotFound($"Categoria id {id} não encontrada");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _database.tb_categoria.FirstOrDefaultAsync(c => c.Id == id);

                if (categoria != null)
                {
                    _database.tb_categoria.Remove(categoria);
                    await _database.SaveChangesAsync();

                    return NoContent();
                }

                return NotFound($"Categoria id {id} não encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }


}
