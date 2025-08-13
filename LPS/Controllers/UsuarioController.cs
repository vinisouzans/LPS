using AutoMapper;
using LPS.Data;
using LPS.DTOs.Usuario;
using LPS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Loja)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<UsuarioReadDTO>>(usuarios));
        }

        // GET api/usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioReadDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Loja)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound();

            return Ok(_mapper.Map<UsuarioReadDTO>(usuario));
        }

        // POST api/usuario        
        [HttpPost]
        public async Task<ActionResult<UsuarioReadDTO>> CreateUsuario(UsuarioCreateDTO dto)
        {
            var lojaExiste = await _context.Lojas.AnyAsync(l => l.Id == dto.LojaId);
            if (!lojaExiste)
                return BadRequest($"A loja com ID {dto.LojaId} não existe.");

            var usuario = _mapper.Map<Usuario>(dto);
            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarioRead = _mapper.Map<UsuarioReadDTO>(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioRead.Id }, usuarioRead);
        }

        // PUT api/usuario/id
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUsuario(int id, UsuarioUpdateDTO dto)
        //{
        //    var usuario = await _context.Usuarios.FindAsync(id);

        //    if (usuario == null)
        //        return NotFound();

        //    // Atualiza os outros campos
        //    _mapper.Map(dto, usuario);

        //    // Se a senha foi enviada e não está vazia, faz o hash
        //    if (!string.IsNullOrWhiteSpace(dto.Senha))
        //    {
        //        usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        //    }

        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // PUT api/usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UsuarioUpdateDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            // Atualiza outros campos
            _mapper.Map(dto, usuario);

            // Só hashea se houver senha informada
            if (!string.IsNullOrWhiteSpace(dto.Senha))
            {
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
            }

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Dados do usuário atualizados com sucesso." });
        }



        // DELETE api/usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
