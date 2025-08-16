using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPS.Models
{
    public class Desconto
    {
        [Key]
        public int Id { get; set; }

        // Percentual de desconto (ex: 5 = 5%)
        [Required]
        public decimal Percentual { get; set; }

        // Data de início do desconto
        [Required]
        public DateTime DataInicio { get; set; }

        // Data de término do desconto
        [Required]
        public DateTime DataFim { get; set; }

        // Se o desconto for específico para um produto, armazena o Id
        // Se for desconto geral, deixa null
        public int? ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public Produto? Produto { get; set; }
    }
}
