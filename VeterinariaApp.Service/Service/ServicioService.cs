
using Microsoft.EntityFrameworkCore;
using VeterinariaApp.Domain.DTOs;
using VeterinariaApp.Domain.Models;
using VeterinariaApp.Service.Interfaces;

namespace VeterinariaApp.Service.Service
{
    public class ServicioService : IServicioService
    {
        private readonly VeterinariaDbContext _context;

        public ServicioService(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Servicio>> GetAllAsync()
            => await _context.Servicios.ToListAsync();

        public async Task<Servicio?> GetByIdAsync(int id)
            => await _context.Servicios.FindAsync(id);

        public async Task CreateAsync(CreateServicioDto dto)
        {
            var servicio = new Servicio
            {
                Nombre = dto.Nombre,
                Tarifa = dto.Tarifa
            };

            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateServicioDto dto)
        {
            var servicio = await _context.Servicios.FindAsync(dto.Id);
            if (servicio is null)
                throw new Exception("Servicio no encontrado");

            servicio.Nombre = dto.Nombre;
            servicio.Tarifa = dto.Tarifa;

            _context.Servicios.Update(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio is not null)
            {
                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
            }
        }
    }

}
