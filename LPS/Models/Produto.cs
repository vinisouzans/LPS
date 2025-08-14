namespace LPS.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string UnidadeVenda { get; set; } = "g"; // ex: "g" ou "ml"
        public string? Descricao { get; set; }
        

        // Chave estrangeira
        public int FornecedorId { get; set; }

        // Propriedade de navegação
        public Fornecedor Fornecedor { get; set; }

        // Relacionamento com Estoques
        public ICollection<Estoque>? Estoques { get; set; }
    }
}
