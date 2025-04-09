namespace ControlePlus_BackEnd.Dto
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public List<string>? Produtos { get; set; }
    }
}