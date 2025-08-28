namespace LPS.DTOs.Venda
{    
    public class VendaResponseDTO
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public string? ClienteNome { get; set; }
        public string? ClienteCPF { get; set; }
        public List<ItemVendaResponseDTO> Itens { get; set; } = new();
    }

    public class ItemVendaResponseDTO
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public int ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
    }
}
