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
            return Ok(vendas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendaPorId(int id)
        {
            var venda = await _vendaService.ObterVendaPorIdAsync(id);
            if (venda == null)
                return NotFound();

            return Ok(venda);
        }

        [HttpPost]
        public async Task<IActionResult> CriarVenda([FromBody] VendaCreateDTO dto)
        {
            var venda = await _vendaService.CriarVendaAsync(dto);
            return CreatedAtAction(nameof(GetVendaPorId), new { id = venda.Id }, venda);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda(int id, VendaUpdateDTO venda)
        {
            try
            {
                await _vendaService.AtualizarVendaAsync(id, venda);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetVendasPorClienteId(int clienteId)
        {
            var vendas = await _vendaService.ObterVendasPorClienteIdAsync(clienteId);

            if (!vendas.Any())
                return NotFound($"Nenhuma venda encontrada para o cliente com ID {clienteId}.");

            return Ok(vendas);
        }

        [HttpGet("cliente/cpf/{cpf}")]
        public async Task<IActionResult> GetVendasPorClienteCpf(string cpf)
        {
            var vendas = await _vendaService.ObterVendasPorClienteCpfAsync(cpf);

            if (!vendas.Any())
                return NotFound($"Nenhuma venda encontrada para o cliente com CPF {cpf}.");

            return Ok(vendas);
        }

    }
}
