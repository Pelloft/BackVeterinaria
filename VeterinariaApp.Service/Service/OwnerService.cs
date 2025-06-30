

using Microsoft.EntityFrameworkCore;
using VeterinariaApp.Domain.DTOs;
using VeterinariaApp.Domain.Models;
using VeterinariaApp.Service.Interfaces;

namespace VeterinariaApp.Service
{
    public class OwnerService : IOwnerService
    {
        private readonly VeterinariaDbContext _context;

        public OwnerService(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Owner>> GetAllAsync()
        {
            return await _context.Owners.Include(o => o.Mascotas).ToListAsync();
        }

        public async Task<Owner?> GetByIdAsync(int id)
        {
            return await _context.Owners
                .Include(o => o.Mascotas)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateOwnerDto dto)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
                throw new Exception("Dueño no encontrado.");

            owner.Nombre = dto.Nombre;
            owner.Apellido = dto.Apellido;
            owner.Telefono = dto.Telefono;
            owner.Correo = dto.Correo;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null) return;

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
        }    
    }
}

