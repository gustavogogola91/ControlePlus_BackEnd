using static BCrypt.Net.BCrypt;

namespace Backend.services
{
    public class EncryptService : IEncryptService
    {
        public string HashSenha(string senha)
        {
            int workFactor = 12;

            var hash = HashPassword(senha, workFactor: workFactor);

            return hash;
        }

        public bool VerificarSenha(string senha, string hash)
        {
            return Verify(senha, hash);
        }
    }
}