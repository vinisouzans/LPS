using System.ComponentModel.DataAnnotations.Schema;

namespace LPS.Models
{
    [Table("Vendas")]
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.UtcNow;
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

        // Relacionamento com Produto
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        // Relacionamento com Estoque
        public int EstoqueId { get; set; }
        public Estoque Estoque { get; set; } = null!;

        // 🔗 Relação com Cliente
        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
