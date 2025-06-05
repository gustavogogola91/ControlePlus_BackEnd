using System.ComponentModel.DataAnnotations;

namespace Backend.Dto
{
    public class LoginDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Senha { get; set; }
    }
}