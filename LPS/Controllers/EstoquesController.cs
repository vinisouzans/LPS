using AutoMapper;
using LPS.Data;
using LPS.DTOs;
using LPS.DTOs.Estoque;
using LPS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoquesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EstoquesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/estoques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstoqueDTO>>> GetEstoques()
        {
            var estoques = await _context.Estoques.ToListAsync();
            return Ok(_mapper.Map<List<EstoqueDTO>>(estoques));
        }

        // GET: api/estoques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstoqueDTO>> GetEstoque(int id)
        {
            var estoque = await _context.Estoques.FindAsync(id);

            if (estoque == null)
                return NotFound(new { message = "Estoque não encontrado." });

            return Ok(_mapper.Map<EstoqueDTO>(estoque));
        }

        // POST: api/estoques
        [HttpPost]
        public async Task<ActionResult<EstoqueDTO>> PostEstoque(EstoqueDTO estoqueDto)
        {
            // Verifica se o produto existe
            var produto = await _context.Produtos.FindAsync(estoqueDto.ProdutoId);
            if (produto == null)
                return BadRequest(new { message = "Produto não encontrado." });

            // Verifica se o fornecedor existe
            var fornecedor = await _context.Fornecedores.FindAsync(estoqueDto.FornecedorId);
            if (fornecedor == null)
                return BadRequest(new { message = "Fornecedor não encontrado." });

            var estoque = _mapper.Map<Estoque>(estoqueDto);
            _context.Estoques.Add(estoque);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstoque), new { id = estoque.Id }, _mapper.Map<EstoqueDTO>(estoque));
        }

        // PUT: api/estoques/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstoque(int id, EstoqueDTO estoqueDto)
        {
            var estoqueExistente = await _context.Estoques.FindAsync(id);
            if (estoqueExistente == null)
                return NotFound(new { message = "Estoque não encontrado." });

            // Verifica se o produto existe
            var produto = await _context.Produtos.FindAsync(estoqueDto.ProdutoId);
            if (produto == null)
                return BadRequest(new { message = "Produto não encontrado." });

            // Verifica se o fornecedor existe
            var fornecedor = await _context.Fornecedores.FindAsync(estoqueDto.FornecedorId);
            if (fornecedor == null)
                return BadRequest(new { message = "Fornecedor não encontrado." });

            _mapper.Map(estoqueDto, estoqueExistente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/estoques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstoque(int id)
        {
            var estoque = await _context.Estoques.FindAsync(id);
            if (estoque == null)
                return NotFound(new { message = "Estoque não encontrado." });

            _context.Estoques.Remove(estoque);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
