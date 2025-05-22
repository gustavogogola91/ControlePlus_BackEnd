

namespace ControlePlus_BackEnd.Dto
{
    public class SetorDetalhadoDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public UsuarioDTO? Usuario { get; set; }

        public ICollection<UsuarioDTO>? Usuarios {get; set;}

        public ICollection<ProdutoDTO>? Produtos {get;set;}

    }
}