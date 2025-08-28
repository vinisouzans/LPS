using AutoMapper;
using LPS.Data;
using LPS.DTOs.Loja;
using LPS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LojaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LojaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/loja
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LojaReadDTO>>> GetLojas()
        {
            var lojas = await _context.Lojas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<LojaReadDTO>>(lojas));
        }

        // GET api/loja/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LojaReadDTO>> GetLoja(int id)
        {
            var loja = await _context.Lojas.FindAsync(id);

            if (loja == null)
                return NotFound(new { mensagem = "Loja não encontrada." });

            return Ok(_mapper.Map<LojaReadDTO>(loja));
        }

        // POST api/loja
        //[Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<LojaReadDTO>> CreateLoja(LojaCreateDTO dto)
        {
            var loja = _mapper.Map<Loja>(dto);

            _context.Lojas.Add(loja);
            await _context.SaveChangesAsync();

            var lojaRead = _mapper.Map<LojaReadDTO>(loja);

            return CreatedAtAction(nameof(GetLoja), new { id = lojaRead.Id },
                new { mensagem = "Loja criada com sucesso.", dados = lojaRead });
        }

        // PUT api/loja/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoja(int id, LojaUpdateDTO dto)
        {
            var loja = await _context.Lojas.FindAsync(id);

            if (loja == null)
                return NotFound(new { mensagem = "Loja não encontrada." });

            _mapper.Map(dto, loja);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Dados da loja atualizados com sucesso." });
        }

        // DELETE api/loja/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoja(int id)
        {
            var loja = await _context.Lojas.FindAsync(id);

            if (loja == null)
                return NotFound(new { mensagem = "Loja não encontrada." });

            _context.Lojas.Remove(loja);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Loja excluída com sucesso." });
        }
    }
}
