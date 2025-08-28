using System.ComponentModel.DataAnnotations.Schema;

namespace LPS.Models
{
    [Table("ItensVenda")]
    public class ItemVenda
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Relacionamento com Venda
        public int VendaId { get; set; }
        public Venda Venda { get; set; } = null!;

        // Relacionamento com Produto
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

    }
}