using LPS.Data;
using Microsoft.EntityFrameworkCore;

namespace LPS.Services
{
    public class DashboardVendasService
    {
        private readonly AppDbContext _context;

        public DashboardVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> ObterResumoVendasAsync(DateTime? dataInicio, DateTime? dataFim)
        {
            // Se só passar uma data, usamos como início e fim do dia
            if (dataInicio.HasValue && !dataFim.HasValue)
            {
                dataFim = dataInicio.Value.Date.AddDays(1).AddTicks(-1);
                dataInicio = dataInicio.Value.Date;
            }

            // Se não passar nada, pega tudo
            if (!dataInicio.HasValue && !dataFim.HasValue)
            {
                dataInicio = DateTime.MinValue;
                dataFim = DateTime.MaxValue;
            }

            var vendasPorProduto = await _context.Vendas
                .Include(v => v.Produto)
                .Where(v =>
                    v.DataVenda >= dataInicio.Value &&
                    v.DataVenda <= dataFim.Value)
                .GroupBy(v => new { v.ProdutoId, v.Produto.Nome })
                .Select(g => new
                {
                    ProdutoId = g.Key.ProdutoId,
                    ProdutoNome = g.Key.Nome,
                    QuantidadeVendida = g.Sum(v => v.Quantidade),
                    ValorTotal = g.Sum(v => v.Quantidade * v.ValorUnitario)
                })
                .ToListAsync();

            var valorTotalPeriodo = vendasPorProduto.Sum(v => v.ValorTotal);

            return new
            {
                DataInicio = dataInicio.Value,
                DataFim = dataFim.Value,
                VendasPorProduto = vendasPorProduto,
                ValorTotalPeriodo = valorTotalPeriodo
            };
        }
    }
}
