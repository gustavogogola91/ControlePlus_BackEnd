namespace Backend.Dto
{
    public class ProdutoPutDTO
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int SetorId { get; set; }
        public int CategoriaId { get; set; }
        public int FornecedorId { get; set; }
         public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
    }
}