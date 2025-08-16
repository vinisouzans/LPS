namespace LPS.DTOs.Venda
{
    public class VendaCreateDTO
    {
        public DateTime DataVenda { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public int ProdutoId { get; set; }
        public int EstoqueId { get; set; }

        // 🔗 Cliente pelo CPF
        public string? ClienteCPF { get; set; }
    }
}
