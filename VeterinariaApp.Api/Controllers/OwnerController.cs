using Microsoft.AspNetCore.Mvc;
using VeterinariaApp.Service.Interfaces;
using VeterinariaApp.Domain.Models;
using VeterinariaApp.Domain.DTOs;

namespace VeterinariaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }
        [HttpGet("Listar")]
        public async Task<IActionResult> GetOwners()
        {
            var owners = await _ownerService.GetAllAsync();
            return Ok(owners);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CreateOwner([FromBody] CreateOwnerDto dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var owner = new Owner
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Correo = dto.Correo
            };

            await _ownerService.CreateAsync(owner);
            return Ok("Dueño registrado correctamente");
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> UpdateOwner(int id, [FromBody] UpdateOwnerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _ownerService.UpdateAsync(id, dto);
                return Ok("Dueño actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await _ownerService.GetByIdAsync(id);
            if (owner is null)
                return NotFound($"No se encontrpo el dueño con el ID {id}");

            await _ownerService.DeleteAsync(id);
            return Ok($"Dueño con ID {id} eliminado correctamente.");
        }

    }
}