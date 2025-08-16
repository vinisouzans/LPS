namespace LPS.DTOs.Venda
{
    public class VendaDTO
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }

        // Cliente vinculado
        public string? ClienteNome { get; set; }
        public string? ClienteCPF { get; set; }

        // Itens da venda
        public List<VendaItemDTO> Itens { get; set; } = new List<VendaItemDTO>();
    }

    public class VendaItemDTO
    {
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public int ProdutoId { get; set; }
        public int EstoqueId { get; set; }
    }
}
