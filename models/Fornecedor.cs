namespace ControlePlus_BackEnd.models
{
    public class Fornecedor
    {
        public Fornecedor(string nome, string contato, string endereco)
        {
            this.nome = nome;
            this.contato = contato;
            this.endereco = endereco;
        }

        public int id { get; set; }
        public string nome { get; set; }
        public string contato { get; set;}
        public string endereco { get; set; }

        public ICollection<Produto>? Produtos;
    }
}