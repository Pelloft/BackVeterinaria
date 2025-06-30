using Microsoft.AspNetCore.Mvc;
using VeterinariaApp.Domain.Models;
using VeterinariaApp.Service.Interfaces;
using VeterinariaApp.Domain.DTOs;

namespace VeterinariaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetPets()
        {
            var pets = await _petService.GetAllAsync();
            return Ok(pets);
        }

        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var pet = await _petService.GetByIdAsync(id);
            if (pet is null)
                return NotFound("Mascota no encontrada.");
            return Ok(pet);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _petService.CreateAsync(dto);
                return Ok("Mascota registrada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> UpdatePet(int id, [FromBody] Pet pet)
        {
            if (id != pet.Id)
                return BadRequest("El ID en la URL no coincide con el del objeto.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingPet = await _petService.GetByIdAsync(id);
            if (existingPet is null)
                return NotFound("Mascota no encontrada.");

            await _petService.UpdateAsync(pet);
            return Ok("Mascota actualizada correctamente.");
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _petService.GetByIdAsync(id);
            if (pet is null)
                return NotFound("Mascota no encontrada.");

            await _petService.DeleteAsync(id);
            return Ok("Mascota eliminada correctamente.");
        }
    }
}
