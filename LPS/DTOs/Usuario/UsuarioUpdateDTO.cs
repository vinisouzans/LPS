namespace LPS.DTOs.Usuario
{
    public class UsuarioUpdateDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Senha { get; set; }
        public int LojaId { get; set; }
    }
}
