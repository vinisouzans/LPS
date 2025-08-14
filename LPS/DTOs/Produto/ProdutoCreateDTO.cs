namespace LPS.DTOs.Produto
{
    public class ProdutoCreateDTO
    {
        public string Nome { get; set; }
        public string Unidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public int FornecedorId { get; set; }
        public int EstoqueInicial { get; set; } // quantidade comprada
    }
}
