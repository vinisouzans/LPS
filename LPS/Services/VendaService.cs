using LPS.Data;
using LPS.Models;
using Microsoft.EntityFrameworkCore;
using LPS.DTOs.Venda;

namespace LPS.Services
{
    public class VendaService
    {
        private readonly AppDbContext _context;

        public VendaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Venda>> ListarVendasAsync()
        {
            return await _context.Vendas
                .Include(v => v.Estoque)
                .Include(v => v.Produto)
                .ToListAsync();
        }

        public async Task<Venda?> ObterVendaPorIdAsync(int id)
        {
            return await _context.Vendas
                .Include(v => v.Produto)
                .Include(v => v.Estoque)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Venda> CriarVendaAsync(VendaCreateDTO dto)
        {
            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.Id == dto.EstoqueId);

            if (estoque == null)
                throw new Exception("Estoque não encontrado");

            if (estoque.Quantidade < dto.Quantidade)
                throw new Exception("Quantidade insuficiente no estoque");

            estoque.Quantidade -= dto.Quantidade;

            var venda = new Venda
            {
                DataVenda = dto.DataVenda,
                Quantidade = dto.Quantidade,
                ValorUnitario = dto.ValorUnitario,
                ValorTotal = dto.Quantidade * dto.ValorUnitario,
                ProdutoId = dto.ProdutoId,
                EstoqueId = dto.EstoqueId
            };

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            return venda;
        }

        public async Task AtualizarVendaAsync(int id, VendaUpdateDTO dto)
        {
            var vendaExistente = await _context.Vendas
                .Include(v => v.Estoque)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vendaExistente == null)
                throw new Exception("Venda não encontrada.");

            var estoque = await _context.Estoques.FindAsync(dto.EstoqueId);
            if (estoque == null)
                throw new Exception("Estoque não encontrado.");

            // Repor quantidade antiga no estoque
            vendaExistente.Estoque.Quantidade += vendaExistente.Quantidade;

            // Verificar se a nova quantidade pode ser descontada
            if (dto.Quantidade > estoque.Quantidade)
                throw new Exception("Quantidade insuficiente em estoque.");

            // Atualizar dados
            vendaExistente.ProdutoId = dto.ProdutoId;
            vendaExistente.EstoqueId = dto.EstoqueId;
            vendaExistente.Quantidade = dto.Quantidade;
            vendaExistente.ValorUnitario = dto.ValorUnitario;
            vendaExistente.ValorTotal = dto.Quantidade * dto.ValorUnitario;

            // Descontar do estoque
            estoque.Quantidade -= dto.Quantidade;

            await _context.SaveChangesAsync();
        }

        public async Task DeletarVendaAsync(int id)
        {
            var venda = await _context.Vendas
                .Include(v => v.Estoque)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venda == null)
                throw new Exception("Venda não encontrada.");

            // Devolver quantidade ao estoque
            venda.Estoque.Quantidade += venda.Quantidade;

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
        }
    }
}
