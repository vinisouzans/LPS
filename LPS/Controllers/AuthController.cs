using LPS.Data;
using LPS.DTOs.Auth;
using LPS.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly JwtSettings _jwtSettings;        

        public AuthController(AppDbContext context, IConfiguration config, JwtSettings jwtSettings)
        {
            _context = context;
            _config = config;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO dto)
        {
            var user = await _context.Usuarios
                .Include(u => u.Loja)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, user.SenhaHash))
                return Unauthorized(new { mensagem = "Credenciais inválidas." });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiryHours);            

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Role, user.Role),        // IMPORTANTE p/ [Authorize(Roles="...")]
                new Claim("lojaId", user.LojaId.ToString())   // útil p/ filtros por loja
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResponseDTO
            {
                Token = tokenString,
                ExpiraEm = expires,
                Nome = user.Nome,
                Role = user.Role,
                LojaId = user.LojaId
            });
        }
    }
}
