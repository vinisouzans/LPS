using System.ComponentModel.DataAnnotations.Schema;

namespace LPS.Models
{
    [Table("Fornecedor")]
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? CNPJ { get; set; }
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }        

        public ICollection<Estoque>? Estoques { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
