using AutoMapper;
using LPS.Data;
using LPS.DTOs;
using LPS.DTOs.Produto;
using LPS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProdutosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return Ok(_mapper.Map<List<ProdutoDTO>>(produtos));
        }

        // GET: api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound(new { message = "Produto não encontrado." });

            return Ok(_mapper.Map<ProdutoDTO>(produto));
        }

        // POST: api/produtos
        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> PostProduto(ProdutoDTO produtoDto)
        {
            // Verifica se o fornecedor existe
            var fornecedor = await _context.Fornecedores.FindAsync(produtoDto.FornecedorId);
            if (fornecedor == null)
                return BadRequest(new { message = "Fornecedor não encontrado." });

            var produto = _mapper.Map<Produto>(produtoDto);
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, _mapper.Map<ProdutoDTO>(produto));
        }

        // PUT: api/produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, ProdutoDTO produtoDto)
        {
            var produtoExistente = await _context.Produtos.FindAsync(id);
            if (produtoExistente == null)
                return NotFound(new { message = "Produto não encontrado." });

            // Verifica se o fornecedor existe
            var fornecedor = await _context.Fornecedores.FindAsync(produtoDto.FornecedorId);
            if (fornecedor == null)
                return BadRequest(new { message = "Fornecedor não encontrado." });

            _mapper.Map(produtoDto, produtoExistente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound(new { message = "Produto não encontrado." });

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
