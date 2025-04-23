namespace ControlePlus_BackEnd.models
{
    public class Setor
    {
        public Setor() { }

        public Setor(string nome, int usuarioId)
        {
            Nome = nome;
            UsuarioId = usuarioId;
        }

        // [Key]
        public int Id { get; set; }

        // [Required(ErrorMessage = "Este campo é obrigatório")]
        // [MaxLength(75, ErrorMessage = "O nome precisa ter no máximo 75 caracteres")]
        public string Nome { get; set; }
        public int? UsuarioId { get; set; }

        public Usuario? Responsavel { get; set; }

        public ICollection<Usuario>? Usuarios { get; set; }

        public ICollection<Produto>? Produtos { get; set; }

    }
}