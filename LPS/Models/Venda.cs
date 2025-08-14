namespace LPS.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        public int? LoteId { get; set; }
        public Estoque? Lote { get; set; }

        public decimal Quantidade { get; set; } // quantidade vendida
        public decimal PrecoVenda { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.UtcNow;
    }
}
