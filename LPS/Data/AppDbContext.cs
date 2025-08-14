using LPS.Models;
using Microsoft.EntityFrameworkCore;

namespace LPS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Produto → Fornecedor (sem delete em cascata)
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Fornecedor)
                .WithMany(f => f.Produtos)
                .HasForeignKey(p => p.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Estoque → Fornecedor (sem delete em cascata)
            modelBuilder.Entity<Estoque>()
                .HasOne(e => e.Fornecedor)
                .WithMany(f => f.Estoques)
                .HasForeignKey(e => e.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Estoque → Produto (sem delete em cascata)
            modelBuilder.Entity<Estoque>()
                .HasOne(e => e.Produto)
                .WithMany(p => p.Estoques)
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurações de decimal para evitar truncamento
            modelBuilder.Entity<Estoque>()
                .Property(e => e.QuantidadeTotal)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<Estoque>()
                .Property(e => e.QuantidadeDisponivel)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<Estoque>()
                .Property(e => e.PrecoCompra)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Venda>()
                .Property(v => v.Quantidade)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<Venda>()
                .Property(v => v.PrecoVenda)
                .HasColumnType("decimal(18,2)");
        }
    }
}
