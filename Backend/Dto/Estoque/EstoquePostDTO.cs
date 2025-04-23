namespace ControlePlus_BackEnd.Dto
{
    public class EstoquePostDTO
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public int QuantidadeAlerta { get; set; }
        public int QuantidadeVendidos { get; set; }
    }
}