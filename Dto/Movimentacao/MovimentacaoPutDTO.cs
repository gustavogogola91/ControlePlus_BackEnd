using ControlePlus_BackEnd.Enums;

namespace ControlePlus_BackEnd.Dto
{
    public class MovimentacaoPutDTO
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public TipoMov Tipo { get; set; }
        public string? Observacao { get; set; }
    }
}