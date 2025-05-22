using ControlePlus_BackEnd.Enums;

namespace Backend.Dto
{
    public class PedidoPutDTO
    {
        public List<int>? ProdutoIds { get; set; }
        public int[]? NumeroAdquirido { get; set; }
        public decimal ValorTotal { get; set; }
        public Status Status { get; set; }
    }
}