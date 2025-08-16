using System.ComponentModel.DataAnnotations;

namespace LPS.DTOs.Cliente
{
    public class ClienteCreateDTO
    {
        [Required, MaxLength(150)]
        public string Nome { get; set; }

        [Required, MaxLength(11)]
        public string CPF { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }
    }
}
