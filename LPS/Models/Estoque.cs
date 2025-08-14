namespace LPS.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; } = null!;

        public string Marca { get; set; } = null!;
        public decimal QuantidadeTotal { get; set; }      // em unidade de compra (ex: 25kg)
        public decimal QuantidadeDisponivel { get; set; } // diminui conforme vendas
        public decimal PrecoCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime? DataValidade { get; set; }

        public ICollection<Venda>? Vendas { get; set; }
    }
}
