namespace ControlePlus_BackEnd.Dto
{
    public class UsuarioPostDTO
    {
        public required string Username { get; set; }
        public required string Nome { get; set; }
        public int? SetorId { get; set; }
        public required string Senha { get; set; }
        public int? UsuarioId { get; set; }

    }


}