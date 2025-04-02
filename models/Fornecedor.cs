namespace ControlePlus_BackEnd.models
{
    public class Fornecedor
    {
        public Fornecedor() { }

        public Fornecedor(string nome, string contato, string endereco)
        {
            Nome = nome;
            Contato = contato;
            Endereco = endereco;
        }

        public int id { get; set; }
        public string Nome { get; set; }
        public string Contato { get; set; }
        public string Endereco { get; set; }

        public ICollection<Produto>? Produtos;
    }
}