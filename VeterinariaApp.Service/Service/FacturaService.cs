
using VeterinariaApp.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace VeterinariaApp.Service.Service
{
    public class FacturaService
    {
        private readonly VeterinariaDbContext _context;
        public FacturaService (VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Factura>> GetFacturasAsync()
        {
            return await _context.Facturas
                .AsNoTracking()
                .OrderByDescending(f => f.Fecha)
                .ToListAsync();
        }

    }
}
