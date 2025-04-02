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

        public int Id { get; set; }
        public string Nome { get; set; }
        public int UsuarioId { get; set; }

        public Usuario? Responsavel { get; set; }

        public ICollection<Usuario>? Usuarios { get; set; }

        public ICollection<Produto>? Produtos { get; set; }

    }
}