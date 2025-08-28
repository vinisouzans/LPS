using Microsoft.AspNetCore.Mvc;
using LPS.Models;
using LPS.Services;
using LPS.DTOs.Venda;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly VendaService _vendaService;

        public VendaController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVendas()
        {
            var vendas = await _vendaService.ListarVendasAsync();
            
            var response = vendas.Select(v => new VendaResponseDTO
            {
                Id = v.Id,
                DataVenda = v.DataVenda,
                ValorTotal = v.ValorTotal,
                ClienteNome = v.Cliente?.Nome,
                ClienteCPF = v.Cliente?.CPF,
                Itens = v.Itens.Select(i => new ItemVendaResponseDTO
                {
                    Id = i.Id,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    Subtotal = i.Subtotal,
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.Produto.Nome
                }).ToList()
            }).ToList();

            return Ok(response);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetVendasPorClienteId(int clienteId)
        {
            var vendas = await _vendaService.ObterVendasPorClienteIdAsync(clienteId);

            if (!vendas.Any())
                return NotFound($"Nenhuma venda encontrada para o cliente com ID {clienteId}.");

            // Mapear para DTOs sem ciclos
            var response = vendas.Select(v => new VendaResponseDTO
            {
                Id = v.Id,
                DataVenda = v.DataVenda,
                ValorTotal = v.ValorTotal,
                ClienteNome = v.Cliente?.Nome,
                ClienteCPF = v.Cliente?.CPF,
                Itens = v.Itens.Select(i => new ItemVendaResponseDTO
                {
                    Id = i.Id,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    Subtotal = i.Subtotal,
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.Produto.Nome
                }).ToList()
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendaPorId(int id)
        {
            var venda = await _vendaService.ObterVendaPorIdAsync(id);
            if (venda == null)
                return NotFound();
            
            var response = new VendaResponseDTO
            {
                Id = venda.Id,
                DataVenda = venda.DataVenda,
                ValorTotal = venda.ValorTotal,
                ClienteNome = venda.Cliente?.Nome,
                ClienteCPF = venda.Cliente?.CPF,
                Itens = venda.Itens.Select(i => new ItemVendaResponseDTO
                {
                    Id = i.Id,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    Subtotal = i.Subtotal,
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.Produto.Nome
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CriarVenda([FromBody] VendaCreateDTO dto)
        {
            var venda = await _vendaService.CriarVendaAsync(dto);

            // Mapear para DTO sem ciclos
            var response = new VendaResponseDTO
            {
                Id = venda.Id,
                DataVenda = venda.DataVenda,
                ValorTotal = venda.ValorTotal,
                ClienteNome = venda.Cliente?.Nome,
                ClienteCPF = venda.Cliente?.CPF,
                Itens = venda.Itens.Select(i => new ItemVendaResponseDTO
                {
                    Id = i.Id,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    Subtotal = i.Subtotal,
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.Produto.Nome
                }).ToList()
            };

            return CreatedAtAction(nameof(GetVendaPorId), new { id = response.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            try
            {
                await _vendaService.DeletarVendaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Erro ao deletar venda {id}: {ex.Message}");

                return BadRequest(new
                {
                    message = "Erro ao deletar venda.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("cliente/cpf/{cpf}")]
        public async Task<IActionResult> GetVendasPorClienteCpf(string cpf)
        {
            var vendas = await _vendaService.ObterVendasPorClienteCpfAsync(cpf);

            if (!vendas.Any())
                return NotFound($"Nenhuma venda encontrada para o cliente com CPF {cpf}.");

            // Mapear para DTOs sem ciclos
            var response = vendas.Select(v => new VendaResponseDTO
            {
                Id = v.Id,
                DataVenda = v.DataVenda,
                ValorTotal = v.ValorTotal,
                ClienteNome = v.Cliente?.Nome,
                ClienteCPF = v.Cliente?.CPF,
                Itens = v.Itens.Select(i => new ItemVendaResponseDTO
                {
                    Id = i.Id,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    Subtotal = i.Subtotal,
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.Produto.Nome
                }).ToList()
            }).ToList();

            return Ok(response);
        }

    }
}
