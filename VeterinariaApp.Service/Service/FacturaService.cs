
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

        public async Task<Factura?> GetFacturaByIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.Detalles) // Incluimos los detalles
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task CrearFacturaAsync(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateFacturaAsync(Factura factura)
        {
            var existingFactura = await _context.Facturas
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.Id == factura.Id);

            if (existingFactura == null)
                return false;

            // Actualizamos datos de la factura
            existingFactura.Cliente = factura.Cliente;
            existingFactura.Mascota = factura.Mascota;
            existingFactura.Fecha = factura.Fecha;
            existingFactura.Subtotal = factura.Subtotal;
            existingFactura.IVA = factura.IVA;
            existingFactura.Total = factura.Total;

            // 🔄 Manejo de detalles (simplificado: reemplazamos por la nueva lista)
            existingFactura.Detalles.Clear();
            foreach (var detalle in factura.Detalles)
            {
                existingFactura.Detalles.Add(detalle);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFacturaAsync(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (factura == null)
                return false;

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
