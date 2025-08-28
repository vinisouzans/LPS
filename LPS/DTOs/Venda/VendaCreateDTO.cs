namespace LPS.DTOs.Venda
{
    public class VendaCreateDTO
    {
        public DateTime DataVenda { get; set; }
        public string? ClienteCPF { get; set; }
        
        public List<ItemVendaCreateDTO> Itens { get; set; } = new List<ItemVendaCreateDTO>();
    }

    public class ItemVendaCreateDTO
    {
        public int ProdutoId { get; set; }       
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}