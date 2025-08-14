using System.ComponentModel.DataAnnotations.Schema;

namespace LPS.Models
{
    [Table("Produtos")]
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }

        // Relacionamento com Fornecedor
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; } = null!;
    }
}
