using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinariaApp.Domain.Models;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFacturaById(int id)
        {
            try
            {
                var factura = await _facturaService.GetFacturaByIdAsync(id);
                if (factura == null)
                    return NotFound($"No se encontró la factura con ID {id}");

                return Ok(factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CrearFactura([FromBody] Factura factura)
        {
            try
            {
                // Validación inicial
                if (factura == null)
                    return BadRequest("La factura no puede ser nula");
                if (factura.Detalles == null || !factura.Detalles.Any())
                    return BadRequest("La factura debe contener al menos un detalles");

                // Guardar en BD
                await _facturaService.CrearFacturaAsync(factura);
                return Ok(new
                {
                    message = "Factura creada exitosamente",
                    factura
                });
            }
            catch (DbUpdateException dbEx) 
            {
                // Errores relacionados con base de datos (FK, duplicados, etc.)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Error al cargar la factura en la base de datos",
                    datails = dbEx.InnerException?.Message ?? dbEx.Message
                });
            }
            catch (Exception ex)
            {
                //Errores inesperados
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Ocurrio un error inesperado al crear la factura",
                    datails = ex.Message
                });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFactura(int id, [FromBody] Factura factura)
        {
            try
            {
                if (factura == null || factura.Id != id)
                    return BadRequest("Los datos de la factura no son válidos");

                var updated = await _facturaService.UpdateFacturaAsync(factura);
                if (!updated)
                    return NotFound($"No se encontró la factura con ID {id}");

                return Ok(new { message = "Factura actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            try
            {
                var deleted = await _facturaService.DeleteFacturaAsync(id);
                if (!deleted)
                    return NotFound($"No se encontró la factura con ID {id}");

                return Ok(new { message = "Factura eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


    }
}
