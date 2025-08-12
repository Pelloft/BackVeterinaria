

namespace VeterinariaApp.Domain.Models
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Tarifa { get; set; }
        public string? ImagenRuta { get; set; }
    }
}
