
namespace VeterinariaApp.Domain.DTOs
{
    public class CreatePetDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public string Raza { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public int OwnerId { get; set; }
    }
}
