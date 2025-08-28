using System.ComponentModel.DataAnnotations.Schema;

namespace LPS.Models
{
    [Table("Vendas")]
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.UtcNow;
        public decimal ValorTotal { get; set; }

        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
    }
}
