namespace LPS.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;

        public string Role { get; set; } = "vendedor"; // vendedor, gerente, admin

        // Relação com Loja
        public int LojaId { get; set; }
        public Loja Loja { get; set; } = null!;
    }
}
