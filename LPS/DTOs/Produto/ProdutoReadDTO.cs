namespace LPS.DTOs.Produto
{
    public class ProdutoReadDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Unidade { get; set; }      // kg, g, L, etc.
        public decimal PrecoVenda { get; set; }
        public string FornecedorNome { get; set; }

        public decimal QuantidadeDisponivel { get; set; } // soma de todos os Estoques
        public decimal QuantidadeTotal { get; set; }      // soma de todos os Estoques
    }
}
