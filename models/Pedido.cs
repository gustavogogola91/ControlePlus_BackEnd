namespace ControlePlus_BackEnd.models
{
    public class Pedido
    {
        public Pedido() {
                //TODO verificar como receber a lista de objs e transformar em Produto
        }
        public int Id { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
        public int[]? NumeroAdiquirido { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public decimal ValorTotal { get; set; }
        public Status Status { get; set; }
    }
}