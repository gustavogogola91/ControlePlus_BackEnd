namespace Backend.services
{
    public interface IEncryptService
    {
        string HashSenha(string senha);
        bool VerificarSenha(string senha, string hash);
    }
}