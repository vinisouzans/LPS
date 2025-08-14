using AutoMapper;
using LPS.Data;
using LPS.DTOs.Produto;
using LPS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProdutoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/produto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> GetProdutos()
        {
            var produtos = await _context.Produtos
                .Include(p => p.Fornecedor)
                .Include(p => p.Estoques)
                .ToListAsync();

            var produtosRead = produtos.Select(p =>
            {
                var dto = _mapper.Map<ProdutoReadDTO>(p);
                dto.QuantidadeTotal = p.Estoques.Sum(e => e.QuantidadeTotal);
                dto.QuantidadeDisponivel = p.Estoques.Sum(e => e.QuantidadeDisponivel);
                dto.FornecedorNome = p.Fornecedor.Nome;
                return dto;
            });

            return Ok(produtosRead);
        }

        // GET api/produto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoReadDTO>> GetProduto(int id)
        {
            var produto = await _context.Produtos
                .Include(p => p.Fornecedor)
                .Include(p => p.Estoques)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            var dto = _mapper.Map<ProdutoReadDTO>(produto);
            dto.QuantidadeTotal = produto.Estoques.Sum(e => e.QuantidadeTotal);
            dto.QuantidadeDisponivel = produto.Estoques.Sum(e => e.QuantidadeDisponivel);
            dto.FornecedorNome = produto.Fornecedor.Nome;

            return Ok(dto);
        }

        // POST api/produto
        [HttpPost]
        public async Task<ActionResult<ProdutoReadDTO>> CreateProduto(ProdutoCreateDTO dto)
        {
            var produto = _mapper.Map<Produto>(dto);

            // Cria o estoque inicial com quantidade total e disponível
            produto.Estoques = new List<Estoque>
            {
                new Estoque
                {
                    QuantidadeTotal = dto.EstoqueInicial,
                    QuantidadeDisponivel = dto.EstoqueInicial,
                    FornecedorId = dto.FornecedorId
                }
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var produtoRead = _mapper.Map<ProdutoReadDTO>(produto);
            produtoRead.QuantidadeTotal = produto.Estoques.Sum(e => e.QuantidadeTotal);
            produtoRead.QuantidadeDisponivel = produto.Estoques.Sum(e => e.QuantidadeDisponivel);
            produtoRead.FornecedorNome = (await _context.Fornecedores.FindAsync(dto.FornecedorId))?.Nome;

            return CreatedAtAction(nameof(GetProduto), new { id = produtoRead.Id }, produtoRead);
        }

        // PUT api/produto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, ProdutoUpdateDTO dto)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound();

            _mapper.Map(dto, produto);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Produto atualizado com sucesso." });
        }

        // DELETE api/produto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos
                .Include(p => p.Estoques) // incluímos para remover os estoques relacionados
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            _context.Estoques.RemoveRange(produto.Estoques);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Produto removido com sucesso." });
        }
    }
}
