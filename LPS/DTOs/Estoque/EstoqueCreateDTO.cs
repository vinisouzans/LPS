namespace LPS.DTOs.Estoque
{
    public class EstoqueCreateDTO
    {
        public int ProdutoId { get; set; }
        public int FornecedorId { get; set; }
        public string Marca { get; set; } = null!;
        public decimal QuantidadeTotal { get; set; }
        public decimal PrecoCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime? DataValidade { get; set; }
    }
}
