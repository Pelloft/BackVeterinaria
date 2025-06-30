
using Microsoft.EntityFrameworkCore;
using VeterinariaApp.Domain.DTOs;
using VeterinariaApp.Domain.Models;
using VeterinariaApp.Service.Interfaces;


namespace VeterinariaApp.Service.Service
{
    public class PetService : IPetService
    {
        private readonly VeterinariaDbContext _context;

        public PetService(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            return await _context.Pets.Include(p => p.Owner).ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(int id)
        {
            return await _context.Pets.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateAsync(CreatePetDto dto)
        {
            var owner = await _context.Owners.FindAsync(dto.OwnerId);
            if (owner == null)
                throw new Exception("El dueño especificado no existe.");

            var pet = new Pet
            {
                Nombre = dto.Nombre,
                Especie = dto.Especie,
                Raza = dto.Raza,
                FechaNacimiento = dto.FechaNacimiento,
                OwnerId = dto.OwnerId
            };

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
