using ControlePlus_BackEnd.Enums;

namespace Backend.Dto
{
    public class JwtDTO
    {
        public string? Nome { get; set; }
        public Role? Roles { get; set; }
    }
}