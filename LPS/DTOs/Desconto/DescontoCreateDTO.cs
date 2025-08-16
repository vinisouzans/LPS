namespace LPS.DTOs.Desconto
{
    public class DescontoCreateDTO
    {
        public int? ProdutoId { get; set; }
        public int? ClienteId { get; set; }
        public decimal Percentual { get; set; }
        public decimal? ValorFixo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
