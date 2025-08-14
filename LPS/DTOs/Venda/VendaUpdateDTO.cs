namespace LPS.DTOs.Venda
{
    public class VendaUpdateDTO
    {
        public DateTime DataVenda { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public int ProdutoId { get; set; }
        public int EstoqueId { get; set; }
    }
}
