namespace ControlePlus_BackEnd.Dto
{
    public class SetorDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? UsuarioId { get; set; }

        public List<string>? IdsUsuarios {get;set;}
        //TODO: impementar a lista de produtos
    }
}