
using VeterinariaApp.Domain.DTOs;
using VeterinariaApp.Domain.Models;

namespace VeterinariaApp.Service.Interfaces
{
    public interface IServicioService
    {
        Task<List<Servicio>> GetAllAsync();
        Task<Servicio?> GetByIdAsync(int id);
        Task CreateAsync(CreateServicioDto dto);
        Task UpdateAsync(UpdateServicioDto dto);
        Task DeleteAsync(int id);
    }
}
