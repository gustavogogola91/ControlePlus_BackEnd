

namespace ControlePlus_BackEnd.Dto
{
    public class SetorDetalhadoDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? UsuarioId { get; set; }

        public List<UsuarioDTO> Usuarios {get; set;} = [];
        //TODO: impementar a lista de produtos

    }
}