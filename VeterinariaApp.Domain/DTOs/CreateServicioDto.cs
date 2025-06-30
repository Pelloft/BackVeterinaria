

namespace VeterinariaApp.Domain.DTOs
{
    public class CreateServicioDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Tarifa { get; set; }
    }
    public class UpdateServicioDto : CreateServicioDto
    {
        public int Id { get; set; }
    }

}
