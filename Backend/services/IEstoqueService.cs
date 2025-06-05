using ControlePlus_BackEnd.Enums;

namespace Backend.services
{
    public interface IEstoqueService
    {
        Task AlterarEstoque(int produtoId, int quantidade, TipoMov tipo);
        Task CriarEstoque(int produtoId);
    }
}