namespace LPS.DTOs.Desconto
{
    // DTO para criar ou atualizar descontos
    public class DescontoCreateUpdateDTO
    {
        public decimal Percentual { get; set; }      // Percentual de desconto
        public DateTime DataInicio { get; set; }     // Data de início do desconto
        public DateTime DataFim { get; set; }        // Data de término do desconto
        public int? ProdutoId { get; set; }          // Produto específico ou null para desconto geral
    }

    // DTO para exibir descontos
    public class DescontoDTO
    {
        public int Id { get; set; }
        public decimal Percentual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int? ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
    }
}
