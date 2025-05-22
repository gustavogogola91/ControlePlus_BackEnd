namespace ControlePlus_BackEnd.Dto
{
    public class FornecedorDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Contato { get; set; }
        public string? Endereco { get; set; }
        public List<ProdutoDTO>? Produtos { get; set; }
    }
}