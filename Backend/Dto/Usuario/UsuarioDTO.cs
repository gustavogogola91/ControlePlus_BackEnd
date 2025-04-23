
namespace ControlePlus_BackEnd.Dto
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public int? SetorId { get; set; }
        public int? UsuarioId { get; set; }
        public bool Ativo { get; set; }
        
    }
}