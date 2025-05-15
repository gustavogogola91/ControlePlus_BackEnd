using ControlePlus_BackEnd.Enums;

namespace ControlePlus_BackEnd.Dto
{
    public class UsuarioDetalhadoDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Nome { get; set; }
        public SetorDTO? Setor { get; set; }
        public Role? Roles { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public UsuarioDTO? ReponsavelUltimaAtualizacao { get; set; }

    }


}
