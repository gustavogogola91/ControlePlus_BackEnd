using System.ComponentModel.DataAnnotations;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.Enums;

namespace Backend.Dto
{
    public class PedidoPostDTO
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public ICollection<ItemPedidoPostDTO> Produtos { get; set; } = new List<ItemPedidoPostDTO>();
    }
}