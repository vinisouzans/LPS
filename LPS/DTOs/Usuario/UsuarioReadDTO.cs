using System;

namespace LPS.DTOs.Usuario
{
    public class UsuarioReadDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PrimeiroNome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; } = null!;
        public string? RG { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int LojaId { get; set; }
        public bool Ativo { get; set; }
        public string? Sexo { get; set; }
        public string? Rua { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public string NomeLoja { get; set; } = null!;
    }
}
