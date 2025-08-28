using AutoMapper;
using LPS.Data;
using LPS.DTOs.Venda;
using LPS.Models;
using Microsoft.EntityFrameworkCore;

namespace LPS.Services
{
    public class VendaService
    {
        private readonly AppDbContext _context;  
        private readonly IMapper _mapper;

        public VendaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Venda>> ListarVendasAsync()
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                    .ThenInclude(i => i.Produto)
                .Include(v => v.Cliente)
                .ToListAsync();
        }

        public async Task<Venda?> ObterVendaPorIdAsync(int id)
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                    .ThenInclude(i => i.Produto)
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Venda> CriarVendaAsync(VendaCreateDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Validar e buscar cliente se CPF foi fornecido
                int? clienteId = null;
                Cliente? cliente = null;
                if (!string.IsNullOrEmpty(dto.ClienteCPF))
                {
                    cliente = await _context.Clientes
                        .FirstOrDefaultAsync(c => c.CPF == dto.ClienteCPF);

                    if (cliente == null)
                    {
                        throw new Exception($"Cliente com CPF {dto.ClienteCPF} não encontrado");
                    }
                    clienteId = cliente.Id;
                }

                // 2. Validar estoques antes de criar a venda
                foreach (var itemDto in dto.Itens)
                {
                    var estoque = await _context.Estoques
                        .FirstOrDefaultAsync(e => e.ProdutoId == itemDto.ProdutoId);

                    if (estoque == null)
                    {
                        throw new Exception($"Estoque não encontrado para o produto ID {itemDto.ProdutoId}");
                    }

                    if (estoque.Quantidade < itemDto.Quantidade)
                    {
                        throw new Exception($"Quantidade insuficiente no estoque para o produto ID {itemDto.ProdutoId}");
                    }
                }

                // 3. Criar a venda
                var venda = new Venda
                {
                    DataVenda = dto.DataVenda,
                    ClienteId = clienteId,
                    Itens = new List<ItemVenda>()
                };

                // 4. Adicionar itens, aplicar descontos e calcular subtotal
                decimal valorTotal = 0;
                var hoje = DateTime.UtcNow.Date;

                foreach (var itemDto in dto.Itens)
                {
                    var produto = await _context.Produtos.FindAsync(itemDto.ProdutoId);
                    if (produto == null)
                    {
                        throw new Exception($"Produto ID {itemDto.ProdutoId} não encontrado");
                    }

                    // Calcular subtotal sem desconto
                    decimal subtotal = itemDto.Quantidade * itemDto.PrecoUnitario;
                    decimal descontoAplicado = 0;

                    // Aplicar descontos específicos do produto (prioridade máxima)
                    var descontosProduto = await _context.Descontos
                        .Where(d => d.DataInicio <= hoje && d.DataFim >= hoje &&
                                   d.ProdutoId == itemDto.ProdutoId)
                        .ToListAsync();

                    foreach (var desconto in descontosProduto)
                    {
                        descontoAplicado = Math.Max(descontoAplicado, desconto.Percentual);
                    }

                    // Aplicar desconto geral (se não houver desconto específico e se houver cliente)
                    if (descontoAplicado == 0 && clienteId.HasValue)
                    {
                        var descontosGerais = await _context.Descontos
                            .Where(d => d.DataInicio <= hoje && d.DataFim >= hoje &&
                                       !d.ProdutoId.HasValue) // Descontos gerais (sem produto específico)
                            .ToListAsync();

                        foreach (var desconto in descontosGerais)
                        {
                            descontoAplicado = Math.Max(descontoAplicado, desconto.Percentual);
                        }
                    }

                    // Calcular subtotal com desconto
                    decimal subtotalComDesconto = subtotal * (1 - descontoAplicado / 100m);

                    var itemVenda = new ItemVenda
                    {
                        ProdutoId = itemDto.ProdutoId,
                        Quantidade = itemDto.Quantidade,
                        PrecoUnitario = itemDto.PrecoUnitario,
                        Subtotal = subtotalComDesconto
                    };

                    venda.Itens.Add(itemVenda);
                    valorTotal += subtotalComDesconto;

                    // 5. Atualizar estoque
                    var estoque = await _context.Estoques
                        .FirstOrDefaultAsync(e => e.ProdutoId == itemDto.ProdutoId);

                    estoque!.Quantidade -= itemDto.Quantidade;
                    _context.Estoques.Update(estoque);
                }

                venda.ValorTotal = valorTotal;

                _context.Vendas.Add(venda);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return venda;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeletarVendaAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var venda = await _context.Vendas
                    .Include(v => v.Itens)
                    .FirstOrDefaultAsync(v => v.Id == id);

                if (venda == null)
                    throw new Exception("Venda não encontrada.");

                // Devolver quantidade ao estoque para cada item
                foreach (var item in venda.Itens)
                {
                    var estoque = await _context.Estoques
                        .FirstOrDefaultAsync(e => e.ProdutoId == item.ProdutoId);

                    if (estoque != null)
                    {
                        estoque.Quantidade += item.Quantidade;
                        _context.Estoques.Update(estoque);
                    }
                }

                _context.Vendas.Remove(venda);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Venda>> ObterVendasPorClienteIdAsync(int clienteId)
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                    .ThenInclude(i => i.Produto)
                .Include(v => v.Cliente)
                .Where(v => v.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Venda>> ObterVendasPorClienteCpfAsync(string cpf)
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                    .ThenInclude(i => i.Produto)
                .Include(v => v.Cliente)
                .Where(v => v.Cliente != null && v.Cliente.CPF == cpf)
                .ToListAsync();
        }
    }
}