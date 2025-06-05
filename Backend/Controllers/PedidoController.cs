using AutoMapper;
using Backend.Dto;
using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// o item pedido pode ter o ID modificado?

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
                var pedidos = await _database.tb_pedido.Include(p => p.Itens).ThenInclude(i => i.Produto).ThenInclude(p => p.Fornecedor)
                    .Include(p => p.Itens).ThenInclude(i => i.Produto).ThenInclude(p => p.Categoria)
                    .Include(p => p.Itens).ThenInclude(i => i.Produto).ThenInclude(p => p.Setor)
                    .Include(p => p.Usuario).ToListAsync();

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
        public async Task<ActionResult<PedidoDTO>> GetPedidoById(int id)
        {
            try
            {
                var pedido = await _database.tb_pedido.Include(p => p.Itens).ThenInclude(i => i.Produto).ThenInclude(p => p.Fornecedor)
                    .Include(p => p.Itens).ThenInclude(i => i.Produto).ThenInclude(p => p.Categoria)
                    .Include(p => p.Itens).ThenInclude(i => i.Produto).ThenInclude(p => p.Setor)
                    .Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Id == id);

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

                foreach (var prodDTO in dto.Produtos)
                {
                    pedido.Itens.Add(_mapper.Map<ItemPedido>(prodDTO));
                }

                var produtos = await _database.tb_produto.Where(p => dto.Produtos.Select(x => x.ProdutoId).Contains(p.Cod)).ToListAsync();

                pedido.ValorTotal = produtos.Sum(p =>
                {
                    var qtd = dto.Produtos.First(i => i.ProdutoId == p.Cod).Quantidade;
                    return p.PrecoCompra * qtd;
                });

                _database.tb_pedido.Add(pedido);
                await _database.SaveChangesAsync();

                var pedidoDTO = _mapper.Map<PedidoDTO>(pedido);

                return Created("Pedido criado com sucesso", pedidoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("alerta")]
        public async Task<IActionResult> NewPedidoAlerta([FromBody] PedidoAutoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estoques = await _database.tb_estoque.Include(e => e.Produto).Where(e => e.Quantidade <= e.QuantidadeAlerta).ToListAsync();

            List<ItemPedidoPostDTO> ItemsPedido = new List<ItemPedidoPostDTO>();
            var pedido = new Pedido();

            pedido.ValorTotal = 0;

            pedido.UsuarioId = dto.UsuarioId;

            foreach (var estoque in estoques)
            {
                var ItemDTO = new ItemPedidoPostDTO();

                int quantidade = estoque.QuantidadeAlerta + Convert.ToInt16(estoque.QuantidadeAlerta * 0.25);

                ItemDTO.ProdutoId = estoque.ProdutoId;
                ItemDTO.Quantidade = quantidade;

                pedido.ValorTotal += estoque.Produto.PrecoCompra * quantidade;

                var Item = _mapper.Map<ItemPedido>(ItemDTO);

                pedido.Itens.Add(Item);
            }

            _database.tb_pedido.Add(pedido);
            await _database.SaveChangesAsync();

            var pedidoDTO = _mapper.Map<PedidoDTO>(pedido);


            return Ok(pedidoDTO);
        }

        [HttpPost("vazio")]
        public async Task<IActionResult> NewPedidoVazio([FromBody] PedidoAutoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estoques = await _database.tb_estoque.Include(e => e.Produto).Where(e => e.Quantidade == 0).ToListAsync();

            List<ItemPedidoPostDTO> ItemsPedido = new List<ItemPedidoPostDTO>();
            var pedido = new Pedido();

            pedido.ValorTotal = 0;

            pedido.UsuarioId = dto.UsuarioId;

            foreach (var estoque in estoques)
            {
                var ItemDTO = new ItemPedidoPostDTO();

                int quantidade = estoque.QuantidadeAlerta + Convert.ToInt16(estoque.QuantidadeAlerta * 0.25);

                ItemDTO.ProdutoId = estoque.ProdutoId;
                ItemDTO.Quantidade = quantidade;

                pedido.ValorTotal += estoque.Produto.PrecoCompra * quantidade;

                var Item = _mapper.Map<ItemPedido>(ItemDTO);

                pedido.Itens.Add(Item);
            }

            _database.tb_pedido.Add(pedido);
            await _database.SaveChangesAsync();

            var pedidoDTO = _mapper.Map<PedidoDTO>(pedido);


            return Ok(pedidoDTO);
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