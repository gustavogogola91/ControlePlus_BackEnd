using System.ComponentModel.DataAnnotations;

namespace ControlePlus_BackEnd.models
{
    public class Produto
    {

        public Produto(int cod, string nome, string descricao, int setorId, int categoriaId, int fornecedorId, decimal precoCompra, decimal precoVenda)
        {
            Cod = cod;
            Nome = nome;
            Descricao = descricao;
            SetorId = setorId;
            CategoriaId = categoriaId;
            FornecedorId = fornecedorId;
            PrecoCompra = precoCompra;
            PrecoVenda = precoVenda;
        }

        [Key]
        public int Cod { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int SetorId { get; set; }
        public Setor? Setor { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        public Estoque? Estoque { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        ICollection<Movimentacao>? Historico { get; set; }

    }
}