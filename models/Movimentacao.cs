using System.ComponentModel.DataAnnotations;
using ControlePlus_BackEnd.Enums;

namespace ControlePlus_BackEnd.models
{
    public class Movimentacao
    {
        public Movimentacao() { }

        //TODO construtor
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "UsuarioId é obrigatório")]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "ProdutoId é obrigatório")]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Tipo é obrigatória")]
        public TipoMov Tipo { get; set; }
        public string? Observacao { get; set; }
    }
}