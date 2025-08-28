using LPS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardVendasController : ControllerBase
    {
        private readonly DashboardVendasService _dashboardService;

        public DashboardVendasController(DashboardVendasService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        //[HttpGet("resumo")]
        //public async Task<IActionResult> GetResumoVendas(
        //    [FromQuery] DateTime? dataInicio,
        //    [FromQuery] DateTime? dataFim)
        //{
        //    var resumo = await _dashboardService.ObterResumoVendasAsync(dataInicio, dataFim);
        //    return Ok(resumo);
        //}
    }
}
