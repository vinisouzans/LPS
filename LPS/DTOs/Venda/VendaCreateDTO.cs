namespace LPS.DTOs.Venda
{
    public class VendaCreateDTO
    {
        public int ProdutoId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
    }
}
