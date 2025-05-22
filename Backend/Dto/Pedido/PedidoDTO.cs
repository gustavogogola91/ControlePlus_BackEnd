using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.Enums;

namespace Backend.Dto
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public ICollection<ProdutoDTO>? Produtos { get; set; }
        public int[]? NumeroAdiquirido { get; set; }
        public UsuarioDTO? Usuario { get; set; }
        public decimal ValorTotal { get; set; }
        public Status Status { get; set; }
        public DateTime DataPedido { get; set; }
    }
}