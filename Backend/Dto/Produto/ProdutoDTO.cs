namespace ControlePlus_BackEnd.Dto
{
    public class ProdutoDTO
    {
        public int Cod { get; set; }
        public string? Nome { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        public string? SetorNome { get; set; }
        public string? FornecedorNome { get; set; }
        public string? CategoriaNome { get; set; }

    }
}