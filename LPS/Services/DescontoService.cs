using LPS.Data;
using LPS.Models;
using Microsoft.EntityFrameworkCore;
using LPS.DTOs.Desconto;

namespace LPS.Services
{
    public class DescontoService
    {
        private readonly AppDbContext _context;

        public DescontoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DescontoDTO>> ListarDescontosAsync()
        {
            return await _context.Descontos
                .Include(d => d.Produto)
                .Select(d => new DescontoDTO
                {
                    Id = d.Id,
                    Percentual = d.Percentual,
                    DataInicio = d.DataInicio,
                    DataFim = d.DataFim,
                    ProdutoId = d.ProdutoId,
                    ProdutoNome = d.Produto != null ? d.Produto.Nome : null
                })
                .ToListAsync();
        }

        public async Task<DescontoDTO> CriarDescontoAsync(DescontoCreateUpdateDTO dto)
        {
            var desconto = new Desconto
            {
                Percentual = dto.Percentual,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                ProdutoId = dto.ProdutoId
            };

            _context.Descontos.Add(desconto);
            await _context.SaveChangesAsync();

            return new DescontoDTO
            {
                Id = desconto.Id,
                Percentual = desconto.Percentual,
                DataInicio = desconto.DataInicio,
                DataFim = desconto.DataFim,
                ProdutoId = desconto.ProdutoId,
                ProdutoNome = null
            };
        }

        public async Task AtualizarDescontoAsync(int id, DescontoCreateUpdateDTO dto)
        {
            var desconto = await _context.Descontos.FindAsync(id);
            if (desconto == null)
                throw new Exception("Desconto não encontrado.");

            desconto.Percentual = dto.Percentual;
            desconto.DataInicio = dto.DataInicio;
            desconto.DataFim = dto.DataFim;
            desconto.ProdutoId = dto.ProdutoId;

            await _context.SaveChangesAsync();
        }

        public async Task DeletarDescontoAsync(int id)
        {
            var desconto = await _context.Descontos.FindAsync(id);
            if (desconto == null)
                throw new Exception("Desconto não encontrado.");

            _context.Descontos.Remove(desconto);
            await _context.SaveChangesAsync();
        }
    }
}
