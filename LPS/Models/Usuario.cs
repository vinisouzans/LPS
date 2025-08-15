using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPS.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string PrimeiroNome { get; set; } = null!;

        [Required]
        public string Sobrenome { get; set; } = null!;

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string CPF { get; set; } = null!;

        public string? RG { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        public string SenhaHash { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "vendedor"; // vendedor, gerente, admin

        public int LojaId { get; set; }
        public Loja Loja { get; set; } = null!;

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataAlteracao { get; set; }

        public string? Sexo { get; set; }

        public string? Rua { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }
    }
}
