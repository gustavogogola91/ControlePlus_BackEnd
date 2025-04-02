using System.ComponentModel.DataAnnotations;

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

        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Contato é obrigatório")]
        public string Contato { get; set; }
        public string Endereco { get; set; }

        public ICollection<Produto>? Produtos;
    }
}