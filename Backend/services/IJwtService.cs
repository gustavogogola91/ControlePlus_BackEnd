using Backend.Dto;

namespace Backend.services
{
    public interface IJwtService
    {
        string GenerateJWT(JwtDTO jwt);
    }
}