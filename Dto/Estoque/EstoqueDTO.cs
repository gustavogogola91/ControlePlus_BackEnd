namespace ControlePlus_BackEnd.Dto
{
    public class EstoqueDTO
    {
        public int Id { get; set; }
        public ProdutoResumoDTO? Produto { get; set; }
        public int Quantidade { get; set; }
        public int QuantidadeAlerta { get; set; }
        public int QuantidadeVendidos { get; set; }
    }
}