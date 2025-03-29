namespace ControlePlus_BackEnd.models
{
    public class Usuario
    {

        public Usuario()
        {
            Roles = new List<string>();
            Ativo = true;
            DataCriacao = DateTime.Now;
            DataUltimaAtualizacao = DateTime.Now;
        }
        public Usuario(string username, string nome, int setorId, string senha, List<string> roles, DateTime dataCriacao, DateTime dataUltimaAtualização)
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
        public List<string> Roles { get; set; } //TODO decidir o tipo do role
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}