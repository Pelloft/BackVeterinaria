

using Microsoft.Extensions.DependencyInjection;
using VeterinariaApp.Domain.Models;

namespace VeterinariaApp.DataAccess
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<VeterinariaDbContext>();

            if (!context.Servicios.Any())
            {
                context.Servicios.AddRange(
                    new Servicio { Nombre = "Peluquería canina", Tarifa = 20 },
                    new Servicio { Nombre = "Baño medicado", Tarifa = 15 },
                    new Servicio { Nombre = "Baño normal", Tarifa = 10 },
                    new Servicio { Nombre = "Desparasitación", Tarifa = 5 },
                    new Servicio { Nombre = "Vacunación", Tarifa = 8 }
                );
                context.SaveChanges();
            }
        }
    }
}
