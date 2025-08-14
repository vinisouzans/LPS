namespace LPS.DTOs.Fornecedor
{
    public class FornecedorDTO
    {
        public string Nome { get; set; } = null!;
        public string? CNPJ { get; set; }
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
    }
}
