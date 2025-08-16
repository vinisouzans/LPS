using System.ComponentModel.DataAnnotations;

namespace LPS.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; }

        [Required, MaxLength(11)]
        public string CPF { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }
    }
}
