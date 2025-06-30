
using VeterinariaApp.Domain.Models;
using VeterinariaApp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using VeterinariaApp.Domain.DTOs;

namespace VeterinariaApp.Service.Interfaces
{
    public interface IPetService
    {
        Task<List<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(int id);
        Task CreateAsync(CreatePetDto dto);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(int id);
    }
}
