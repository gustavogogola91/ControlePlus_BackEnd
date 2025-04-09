namespace ControlePlus_BackEnd.Dto
{
    public class ProdutoCompletoDTO
    {
        public string? Nome { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        public string? SetorNome { get; set; }
        public string? FornecedorNome { get; set; }
        //NOTE: cod, nome, categoria, fornecedor
    }
}