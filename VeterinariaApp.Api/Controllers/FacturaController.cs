using Microsoft.AspNetCore.Mvc;
using VeterinariaApp.Service.Service;

namespace VeterinariaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaService _facturaService;

        public FacturaController(FacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFacturas()
        {
            try
            {
                var facturas = await _facturaService.GetFacturasAsync();
                if (facturas == null || !facturas.Any())
                    return NoContent();

                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
