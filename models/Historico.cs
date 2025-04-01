using ControlePlus_BackEnd.Enums;

namespace ControlePlus_BackEnd.models
{
    public class Historico
    {
        //TODO construtor
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
        public TipoMov tipo { get; set; }
        public string? observacao { get; set; }
    }
}