
using System.ComponentModel.DataAnnotations;

namespace VeterinariaApp.Domain.DTOs
{
    public class UpdateOwnerDto
    {

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;
    }
}
