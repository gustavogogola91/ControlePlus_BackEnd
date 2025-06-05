using ControlePlus_BackEnd.db;
using ControlePlus_BackEnd.Enums;
using ControlePlus_BackEnd.models;
using Microsoft.EntityFrameworkCore;

namespace Backend.services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly AppDbContext _database;

        public EstoqueService(AppDbContext database)
        {
            _database = database;
        }

        public async Task AlterarEstoque(int produtoId, int quantidade, TipoMov tipo)
        {
            try
            {
                var estoque = await _database.tb_estoque.FirstOrDefaultAsync(e => e.ProdutoId == produtoId);

                if (tipo == 0)
                {
                    estoque!.Quantidade += quantidade;
                }
                else
                {
                    if (estoque!.Quantidade < quantidade)
                    {
                        throw new Exception("Quantidade insuficiente em estoque");
                    }
                    estoque!.Quantidade -= quantidade;
                }

                _database.tb_estoque.Update(estoque);
                await _database.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar o estoque: " + ex.Message, ex);
            }
        }

        public async Task CriarEstoque(int produtoId)
        {
            //             {
            //                 "produtoId": 1,
            //                   "quantidade": 0,
            //                      "quantidadeAlerta": 12,
            //               "quantidadeVendidos": 12
            //                 }

            var estoque = new Estoque();
            estoque.ProdutoId = produtoId;

            _database.tb_estoque.Add(estoque);
            await _database.SaveChangesAsync();
        }
    }
}