using AutoMapper;
using Backend.Dto;
using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//PARA TERMINAR PRECISA DE:
// - USUARIOID

namespace ControlePlus_BackEnd.Controllers
{
    [ApiController]
    [Route("pedido")]
    public class PedidoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _database;

        public PedidoController(IMapper mapper, AppDbContext database)
        {
            _mapper = mapper;
            _database = database;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetAll()
        {
            try
            {
                var pedidos = await _database.tb_pedido.Include(p => p.Produtos).ToListAsync();

                if (pedidos == null || !pedidos.Any())
                {
                    return BadRequest("Não existem pedidos cadastrados.");
                }

                var pedidosDTO = _mapper.Map<List<PedidoDTO>>(pedidos);

                return Ok(pedidosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDTO>> GetUsuarioById(int id)
        {
            try
            {
                var pedido = await _database.tb_pedido.FirstOrDefaultAsync(p => p.Id == id);
                if (pedido == null)
                {
                    return NotFound($"Pedido id {id} não encontrado");
                }

                var pedidoDTO = _mapper.Map<ProdutoDTO>(pedido);
                return Ok(pedidoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


        [HttpPost]
        public async Task<IActionResult> NewPedido([FromBody] PedidoPostDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var pedido = _mapper.Map<Pedido>(dto);
                pedido.DataPedido = DateTime.UtcNow;

                _database.tb_pedido.Add(pedido);
                await _database.SaveChangesAsync();

                var pedidoDTO = _mapper.Map<PedidoDTO>(pedido);

                return Created("", pedidoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyPedido(int id, [FromBody] PedidoPutDTO pedidoMod)
        {
            try
            {
                var pedidoExistente = await _database.tb_pedido.FindAsync(id);
                if (pedidoExistente == null)
                {
                    return NotFound($"Setor id {id} não encontrado");   
                }

                if (pedidoMod.ProdutoIds != null && pedidoMod.ProdutoIds.Any()) 
                {
                    pedidoExistente.ProdutoIds = pedidoMod.ProdutoIds;
                }
                if (pedidoMod.NumeroAdquirido != null && pedidoMod.NumeroAdquirido.Any()) 
                {
                    pedidoExistente.NumeroAdiquirido = pedidoMod.NumeroAdquirido;
                }
                if (pedidoMod.ValorTotal > 0) 
                {
                    pedidoExistente.ValorTotal = pedidoMod.ValorTotal;
                }
                if (pedidoMod.Status != 0) 
                {
                    pedidoExistente.Status = pedidoMod.Status;
                }

                _database.tb_pedido.Update(pedidoExistente);
                await _database.SaveChangesAsync();

                var pedidoDTO = _mapper.Map<PedidoDTO>(pedidoExistente);
                return Ok(pedidoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            try
            {
                var pedido = await _database.tb_pedido.FirstOrDefaultAsync(p => p.Id == id);

                if (pedido != null)
                {
                    _database.Remove(pedido);
                    await _database.SaveChangesAsync();
                    return Ok();
                }

                return NotFound($"Não fpo possível encontrar o pedido com id {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}