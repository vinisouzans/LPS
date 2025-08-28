namespace LPS.DTOs.Venda
{
    public class VendaUpdateDTO
    {
        public DateTime DataVenda { get; set; }
        public string? ClienteCPF { get; set; }

        public List<ItemVendaUpdateDTO> Itens { get; set; } = new List<ItemVendaUpdateDTO>();
    }

    public class ItemVendaUpdateDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}