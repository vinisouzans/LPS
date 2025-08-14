namespace LPS.DTOs.Produto
{
    public class ProdutoDTO
    {
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int FornecedorId { get; set; }
    }
}
