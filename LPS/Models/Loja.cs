namespace LPS.Models
{
    public class Loja
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
    }


