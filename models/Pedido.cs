using ControlePlus_BackEnd.Enums;

namespace ControlePlus_BackEnd.models
{
    public class Pedido
    {
        public Pedido() { 
            DataPedido = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public List<int>? ProdutoIds { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
        public int[]? NumeroAdiquirido { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public decimal ValorTotal { get; set; }
        public Status Status { get; set; }

        public DateTime DataPedido {get; set;}
    }
}