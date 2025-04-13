using ControlePlus_BackEnd.Enums;

namespace ControlePlus_BackEnd.Dto
{
    public class MovimentacaoDTO
    {
        public int Id { get; set; }
        public UsuarioDTO? Usuario { get; set; }
        public ProdutoResumidoDTO? Produto { get; set; }
        public int Quantidade { get; set; }
        public TipoMov Tipo { get; set; }
        public string? Observacao { get; set; }
    }
}