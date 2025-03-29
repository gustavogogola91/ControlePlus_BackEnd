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
    }
}