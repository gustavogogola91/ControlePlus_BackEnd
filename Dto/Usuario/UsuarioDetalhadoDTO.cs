using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto
{
    public class UsuarioDetalhadoDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Nome { get; set; }
        public int SetorId { get; set; }
        //FIXME: implementar ROLE
        public bool Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public int? UsuarioId { get; set; }

    }


}
