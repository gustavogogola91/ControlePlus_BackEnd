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
    public class PedidoController : ControllerBase{
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







    }
}