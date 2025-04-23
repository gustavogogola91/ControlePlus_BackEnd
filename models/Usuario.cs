using ControlePlus_BackEnd.Enums;

namespace ControlePlus_BackEnd.models
{
    public class Usuario
    {

        public Usuario()
        {
            Ativo = true;
            DataCriacao = DateTime.Now;
            DataUltimaAtualizacao = DateTime.Now;
        }
        public Usuario(string username, string nome, int setorId, string senha, Role roles, DateTime dataCriacao, DateTime dataUltimaAtualização)
        {
            Username = username;
            Nome = nome;
            SetorId = setorId;
            Senha = senha;
            Roles = roles;
            Ativo = true;
            DataCriacao = dataCriacao;
            DataUltimaAtualizacao = dataUltimaAtualização;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public int SetorId { get; set; }
        public Setor? Setor { get; set; }
        public string Senha { get; set; }
        public Role Roles { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario? ReponsavelUltimaAtualizacao { get; set; }
    }
}