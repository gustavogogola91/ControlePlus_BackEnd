using System.ComponentModel.DataAnnotations;

namespace Backend.Dto
{
    public class ProdutoPostDTO
    {
        [Required]
        public int Cod { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        public string? Descricao { get; set; }
        [Required]
        public decimal PrecoCompra { get; set; }
        [Required]
        public decimal PrecoVenda { get; set; }
        [Required]
        public int SetorId { get; set; }
        [Required]
        public int FornecedorId { get; set; }
        [Required]
        public int CategoriaId{ get; set; }
    }
}