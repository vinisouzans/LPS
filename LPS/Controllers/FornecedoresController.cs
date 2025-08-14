using AutoMapper;
using LPS.Data;
using LPS.DTOs;
using LPS.DTOs.Fornecedor;
using LPS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FornecedoresController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/fornecedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> GetFornecedores()
        {
            var fornecedores = await _context.Fornecedores.ToListAsync();
            return Ok(_mapper.Map<List<FornecedorDTO>>(fornecedores));
        }

        // GET: api/fornecedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FornecedorDTO>> GetFornecedor(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);

            if (fornecedor == null)
                return NotFound(new { message = "Fornecedor não encontrado." });

            return Ok(_mapper.Map<FornecedorDTO>(fornecedor));
        }

        // POST: api/fornecedores
        [HttpPost]
        public async Task<ActionResult<FornecedorDTO>> PostFornecedor(FornecedorDTO fornecedorDto)
        {
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFornecedor), new { id = fornecedor.Id }, _mapper.Map<FornecedorDTO>(fornecedor));
        }

        // PUT: api/fornecedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFornecedor(int id, FornecedorDTO fornecedorDto)
        {
            var fornecedorExistente = await _context.Fornecedores.FindAsync(id);
            if (fornecedorExistente == null)
                return NotFound(new { message = "Fornecedor não encontrado." });

            _mapper.Map(fornecedorDto, fornecedorExistente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/fornecedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedor(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
                return NotFound(new { message = "Fornecedor não encontrado." });

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
