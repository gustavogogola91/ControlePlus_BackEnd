using System.ComponentModel.DataAnnotations;

namespace ControlePlus_BackEnd.models
{
    public class Categoria
    {

        public Categoria() { }
        public Categoria(string nome)
        {
            Nome = nome;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        public ICollection<Produto>? Produtos { get; set; }
    }
}