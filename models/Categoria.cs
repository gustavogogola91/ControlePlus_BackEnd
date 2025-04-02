namespace ControlePlus_BackEnd.models
{
    public class Categoria
    {

        public Categoria() { }
        public Categoria(string nome)
        {
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Produto>? Produtos { get; set; }
    }
}