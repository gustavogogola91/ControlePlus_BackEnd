using ControlePlus_BackEnd.db;
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
        private readonly AppDbContext _database;

        public PedidoController(AppDbContext database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
            try
            {
                var pedidos = await _database.tb_pedido.ToListAsync();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro aos buscar os pedidos");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetUsuarioById(int id)
        {
            try
            {
                var pedido = await _database.tb_pedido.FirstOrDefaultAsync(p => p.Id == id);
                if (pedido != null)
                {
                    return Ok(pedido);
                }

                return NotFound($"Pedido id {id} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao buscar o pedidos");
            }

        }

        [HttpPost]
        public async Task<IActionResult> NewPedido([FromBody] Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validacao = await _database.tb_usuario.FirstOrDefaultAsync(p => p.Id == pedido.Id);
                if (validacao == null)
                {

                    // pedido.DataCriacao = DateTime.UtcNow;
                    _database.tb_pedido.Add(pedido);
                    await _database.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest($"Já existe um pedido com o Id {pedido.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao adicionar novo pedido");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyPedido(int id, [FromBody] Pedido pedidoMod)
        {
            try
            {
                var pedidoExistente = await _database.tb_usuario.FindAsync(id);
                if (pedidoExistente != null)
                {
                    pedidoExistente.Nome = pedidoMod.Nome;
                    pedidoExistente.Username = pedidoMod.Username;
                    pedidoExistente.SetorId = pedidoMod.SetorId;
                    pedidoExistente.Roles = pedidoMod.Roles;
                    pedidoExistente.Ativo = pedidoMod.Ativo;
                    pedidoExistente.DataUltimaAtualizacao = DateTime.UtcNow;

                    await _database.SaveChangesAsync();
                    return Ok(usuarioExistente);
                }
                return NotFound($"Setor id {id} não encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao modificar o Usuario");
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
                Console.WriteLine(ex);
                return StatusCode(500, "Erro ao deletar pedido");
            }
        }


    


    }
}