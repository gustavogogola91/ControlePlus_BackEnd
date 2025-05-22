using System.ComponentModel.DataAnnotations;
using ControlePlus_BackEnd.Enums;

namespace Backend.Dto
{
    public class PedidoPostDTO
    {
        [Required]
        public List<int>? ProdutoIds { get; set; }
        [Required]
        public int[]? NumeroAdiquirido { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public decimal ValorTotal { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}