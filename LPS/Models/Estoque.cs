using System.ComponentModel.DataAnnotations.Schema;

namespace LPS.Models
{
    [Table("Estoques")]
    public class Estoque
    {
        public int Id { get; set; }
        public decimal Quantidade { get; set; }

        // Relacionamento com Produto
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        // Relacionamento com Fornecedor
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; } = null!;
    }
}
