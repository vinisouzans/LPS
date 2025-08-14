namespace LPS.DTOs.Produto
{
    public class ProdutoUpdateDTO
    {
        public string Nome { get; set; }
        public string Unidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public int FornecedorId { get; set; }
    }
}
