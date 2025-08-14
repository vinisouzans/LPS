using AutoMapper;
using LPS.Data;
using LPS.DTOs;
using LPS.DTOs.Venda;
using LPS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VendasController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/vendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendaDTO>>> GetVendas()
        {
            var vendas = await _context.Vendas.ToListAsync();
            return Ok(_mapper.Map<List<VendaDTO>>(vendas));
        }

        // GET: api/vendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendaDTO>> GetVenda(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);

            if (venda == null)
                return NotFound(new { message = "Venda não encontrada." });

            return Ok(_mapper.Map<VendaDTO>(venda));
        }

        // POST: api/vendas
        [HttpPost]
        public async Task<ActionResult<VendaDTO>> PostVenda(VendaDTO vendaDto)
        {
            // Verifica se o produto existe
            var produto = await _context.Produtos.FindAsync(vendaDto.ProdutoId);
            if (produto == null)
                return BadRequest(new { message = "Produto não encontrado." });

            // Verifica se o estoque existe
            var estoque = await _context.Estoques.FindAsync(vendaDto.EstoqueId);
            if (estoque == null)
                return BadRequest(new { message = "Estoque não encontrado." });

            if (vendaDto.Quantidade > estoque.Quantidade)
                return BadRequest(new { message = "Quantidade insuficiente em estoque." });

            var venda = _mapper.Map<Venda>(vendaDto);
            venda.DataVenda = DateTime.UtcNow;
            venda.ValorTotal = vendaDto.Quantidade * vendaDto.ValorUnitario;

            // Atualiza estoque
            estoque.Quantidade -= vendaDto.Quantidade;

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVenda), new { id = venda.Id }, _mapper.Map<VendaDTO>(venda));
        }

        // PUT: api/vendas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda(int id, VendaDTO vendaDto)
        {
            var vendaExistente = await _context.Vendas
                .Include(v => v.Estoque)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vendaExistente == null)
                return NotFound(new { message = "Venda não encontrada." });

            var estoque = await _context.Estoques.FindAsync(vendaDto.EstoqueId);
            if (estoque == null)
                return BadRequest(new { message = "Estoque não encontrado." });

            // Repor a quantidade antiga no estoque antes de atualizar
            vendaExistente.Estoque.Quantidade += vendaExistente.Quantidade;

            // Verificar se a nova quantidade pode ser descontada
            if (vendaDto.Quantidade > estoque.Quantidade)
                return BadRequest(new { message = "Quantidade insuficiente em estoque." });

            // Atualizar os dados da venda
            vendaExistente.ProdutoId = vendaDto.ProdutoId;
            vendaExistente.EstoqueId = vendaDto.EstoqueId;
            vendaExistente.Quantidade = vendaDto.Quantidade;
            vendaExistente.ValorUnitario = vendaDto.ValorUnitario;
            vendaExistente.ValorTotal = vendaDto.Quantidade * vendaDto.ValorUnitario;

            // Descontar do estoque
            estoque.Quantidade -= vendaDto.Quantidade;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/vendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            var venda = await _context.Vendas
                .Include(v => v.Estoque)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venda == null)
                return NotFound(new { message = "Venda não encontrada." });

            // Devolver quantidade ao estoque
            venda.Estoque.Quantidade += venda.Quantidade;

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
