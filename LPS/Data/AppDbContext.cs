using LPS.Models;
using Microsoft.EntityFrameworkCore;

namespace LPS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Estoque> Estoques { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Produto → Fornecedor (sem delete em cascata)
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Fornecedor)
                .WithMany()
                .HasForeignKey(p => p.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Estoque → Produto
            modelBuilder.Entity<Estoque>()
                .HasOne(e => e.Produto)
                .WithMany()
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Estoque → Fornecedor
            modelBuilder.Entity<Estoque>()
                .HasOne(e => e.Fornecedor)
                .WithMany()
                .HasForeignKey(e => e.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
