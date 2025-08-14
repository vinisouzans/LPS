namespace LPS.DTOs.Estoque
{
    public class EstoqueDTO
    {
        public int Id { get; set; }
        public decimal Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public int FornecedorId { get; set; }
    }
}
