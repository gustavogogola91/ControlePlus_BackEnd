namespace ControlePlus_BackEnd.Dto
{
    public class UsuarioUpdateDTO
    {
        public string? Username { get; set; }
        public string? Nome { get; set; }
        public int? SetorId { get; set; }
        public string? Senha { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioId { get; set; }
    }
}