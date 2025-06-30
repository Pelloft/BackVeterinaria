
using VeterinariaApp.Domain.DTOs;
using VeterinariaApp.Domain.Models;

namespace VeterinariaApp.Service.Interfaces
{
    public interface IOwnerService
    {
        Task<List<Owner>> GetAllAsync();
        Task<Owner?> GetByIdAsync(int id);
        Task CreateAsync(Owner owner);
        Task UpdateAsync(int id, UpdateOwnerDto dto);
        Task DeleteAsync(int id);
    }
}
