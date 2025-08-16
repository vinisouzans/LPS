using Microsoft.AspNetCore.Mvc;
using LPS.Services;
using LPS.DTOs.Desconto;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DescontoController : ControllerBase
    {
        private readonly DescontoService _descontoService;

        public DescontoController(DescontoService descontoService)
        {
            _descontoService = descontoService;
        }

        // GET: api/Desconto
        [HttpGet]
        public async Task<IActionResult> GetDescontos()
        {
            var descontos = await _descontoService.ListarDescontosAsync();
            return Ok(descontos);
        }

        // POST: api/Desconto
        [HttpPost]
        public async Task<IActionResult> CriarDesconto([FromBody] DescontoCreateUpdateDTO dto)
        {
            try
            {
                var desconto = await _descontoService.CriarDescontoAsync(dto);
                return CreatedAtAction(nameof(GetDescontos), new { id = desconto.Id }, desconto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Desconto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarDesconto(int id, [FromBody] DescontoCreateUpdateDTO dto)
        {
            try
            {
                await _descontoService.AtualizarDescontoAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Desconto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarDesconto(int id)
        {
            try
            {
                await _descontoService.DeletarDescontoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
