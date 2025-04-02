using ControlePlus_BackEnd.models;
using Microsoft.EntityFrameworkCore;

namespace ControlePlus_BackEnd.db
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Produto> tb_produto { get; set; }

        public DbSet<Usuario> tb_usuario { get; set; }

        public DbSet<Categoria> tb_categoria { get; set; }

        public DbSet<Setor> tb_setor { get; set; }

        public DbSet<Estoque> tb_estoque { get; set; }

        public DbSet<Pedido> tb_pedido { get; set; }

        public DbSet<Fornecedor> tb_fornecedor { get; set; }

        public DbSet<Movimentacao> tb_movimentacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Setor>()
                .HasOne(s => s.Responsavel)
                .WithMany()
                .HasForeignKey(s => s.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Setor)
                .WithMany(s => s.Usuarios)
                .HasForeignKey(u => u.SetorId);

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Produtos)
                .WithOne()
                .HasForeignKey("PedidoId")
                .IsRequired(false);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.ProdutoIds)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
                );
        }
    }
}
