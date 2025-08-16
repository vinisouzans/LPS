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
                .Include(v => v.Cliente)
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
            // Buscar cliente pelo CPF (opcional, mas necessário para validar desconto geral)
            Cliente? cliente = null;
            if (!string.IsNullOrEmpty(dto.ClienteCPF))
            {
                cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.CPF == dto.ClienteCPF);
                if (cliente == null)
                    throw new Exception("Cliente não encontrado para o CPF informado.");
            }

            var estoque = await _context.Estoques.FirstOrDefaultAsync(e => e.Id == dto.EstoqueId);
            if (estoque == null)
                throw new Exception("Estoque não encontrado");

            if (estoque.Quantidade < dto.Quantidade)
                throw new Exception("Quantidade insuficiente no estoque");

            // Desconta do estoque
            estoque.Quantidade -= dto.Quantidade;

            // Valor bruto da venda
            decimal valorTotal = dto.Quantidade * dto.ValorUnitario;

            // Buscar descontos ativos para a data da venda
            var hoje = dto.DataVenda.Date;
            var descontosAtivos = await _context.Descontos
                .Where(d => d.DataInicio <= hoje && d.DataFim >= hoje)
                .ToListAsync();

            decimal descontoAplicado = 0m;

            foreach (var desconto in descontosAtivos)
            {
                if (desconto.ProdutoId.HasValue && desconto.ProdutoId.Value == dto.ProdutoId)
                {
                    // Desconto específico do produto
                    descontoAplicado = Math.Max(descontoAplicado, desconto.Percentual);
                }
                else if (!desconto.ProdutoId.HasValue && cliente != null)
                {
                    // Desconto geral aplicado a qualquer venda com cliente
                    descontoAplicado = Math.Max(descontoAplicado, desconto.Percentual);
                }
            }

            // Aplicar desconto
            valorTotal = valorTotal * (1 - descontoAplicado / 100m);

            var venda = new Venda
            {
                DataVenda = dto.DataVenda,
                Quantidade = dto.Quantidade,
                ValorUnitario = dto.ValorUnitario,
                ValorTotal = valorTotal,
                ProdutoId = dto.ProdutoId,
                EstoqueId = dto.EstoqueId,
                ClienteId = cliente?.Id
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

            // Atualizar dados básicos
            vendaExistente.ProdutoId = dto.ProdutoId;
            vendaExistente.EstoqueId = dto.EstoqueId;
            vendaExistente.Quantidade = dto.Quantidade;
            vendaExistente.ValorUnitario = dto.ValorUnitario;

            // Buscar cliente pelo CPF (se informado)
            Cliente? cliente = null;
            if (!string.IsNullOrEmpty(dto.ClienteCPF))
            {
                cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.CPF == dto.ClienteCPF);
                if (cliente == null)
                    throw new Exception("Cliente não encontrado para o CPF informado.");
                vendaExistente.ClienteId = cliente.Id;
            }
            else
            {
                vendaExistente.ClienteId = null;
            }

            // Valor bruto da venda
            decimal valorTotal = dto.Quantidade * dto.ValorUnitario;

            // Buscar descontos ativos para a data da venda
            var hoje = vendaExistente.DataVenda.Date;
            var descontosAtivos = await _context.Descontos
                .Where(d => d.DataInicio <= hoje && d.DataFim >= hoje)
                .ToListAsync();

            decimal descontoAplicado = 0m;

            foreach (var desconto in descontosAtivos)
            {
                if (desconto.ProdutoId.HasValue && desconto.ProdutoId.Value == dto.ProdutoId)
                {
                    // Desconto específico do produto
                    descontoAplicado = Math.Max(descontoAplicado, desconto.Percentual);
                }
                else if (!desconto.ProdutoId.HasValue && cliente != null)
                {
                    // Desconto geral aplicado a qualquer venda com cliente
                    descontoAplicado = Math.Max(descontoAplicado, desconto.Percentual);
                }
            }

            // Aplicar desconto
            vendaExistente.ValorTotal = valorTotal * (1 - descontoAplicado / 100m);

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

        public async Task<IEnumerable<Venda>> ObterVendasPorClienteIdAsync(int clienteId)
        {
            return await _context.Vendas
                .Include(v => v.Produto)
                .Include(v => v.Estoque)
                .Include(v => v.Cliente)
                .Where(v => v.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Venda>> ObterVendasPorClienteCpfAsync(string cpf)
        {
            return await _context.Vendas
                .Include(v => v.Produto)
                .Include(v => v.Estoque)
                .Include(v => v.Cliente)
                .Where(v => v.Cliente != null && v.Cliente.CPF == cpf)
                .ToListAsync();
        }

    }
}
