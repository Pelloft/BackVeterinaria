using Microsoft.AspNetCore.Mvc;
using VeterinariaApp.Domain.DTOs;
using VeterinariaApp.Service.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VeterinariaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioService _servicioService;

        public ServicioController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        //public async Task<IActionResult> GetServicios()
        //    => Ok(await _servicioService.GetAllAsync());

        [HttpGet("Listar")]
        public async Task<IActionResult> GetServicios()
        {
            try
            {
                var servicios = await _servicioService.GetAllAsync();

                if (servicios == null || !servicios.Any())
                    return NotFound("No hay servicios disponibles.");

                return Ok(servicios);
            }
            catch (Exception ex)
            {
                // Podrías loguear el error aquí si tienes un logger configurado
                return StatusCode(500, $"Ocurrió un error al obtener los servicios: {ex.Message}");
            }
        }


        //public JsonResponse(T? data = default, string message = "", ResponseStatus status = ResponseStatus.success)
        //{
        //    Status = status;
        //    Message = message;
        //    Data = data;
        //}


        [HttpPost("Crear")]
        public async Task<IActionResult> CreateServicio([FromBody] CreateServicioDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Datos invalidos");
                await _servicioService.CreateAsync(dto);
                return Ok("Servicio creado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el servicio: {ex.Message}");
            }
            
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> UpdateServicio([FromBody] UpdateServicioDto dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                    return BadRequest("Datos inválidos para la actualización.");

                var servicioExistente = await _servicioService.GetByIdAsync(dto.Id);
                if (servicioExistente == null)
                    return NotFound("El servicio a actualizar no existe.");

                await _servicioService.UpdateAsync(dto);
                return Ok("Servicio actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el servicio: {ex.Message}");
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("ID de servicio no válido");
                var servicio = await _servicioService.GetByIdAsync(id);
                if (servicio == null)
                    return NotFound("El servicio a eliminar no existe.");

                await _servicioService.DeleteAsync(id);
                return Ok("Servicio eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el servicio: {ex.Message}");
            }
        }
    }
}