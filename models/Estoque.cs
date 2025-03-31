namespace ControlePlus_BackEnd.models
{
    public class Estoque
    {
        public Estoque(int produtoId, int quantidade, int quantidadeAlerta, int quantidadeVendidos)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            QuantidadeAlerta = quantidadeAlerta;
            QuantidadeVendidos = quantidadeVendidos;
        }

        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
        public int QuantidadeAlerta { get; set; }
        public int QuantidadeVendidos { get; set; }
    }
}