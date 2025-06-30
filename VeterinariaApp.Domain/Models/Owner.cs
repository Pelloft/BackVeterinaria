

using System.ComponentModel.DataAnnotations;

namespace VeterinariaApp.Domain.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(20, ErrorMessage = "El nombre no debe superar los 20 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(20, ErrorMessage = "El apellido no debe superar los 20 caracteres")]
        public string Apellido { get; set; } = null!;

        [Phone(ErrorMessage = "Número de teléfono incorrecto.")]
        public string Telefono { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Correo incorrecto.")]
        public string Correo { get; set; } = null!;

        //Relación
        public ICollection<Pet> Mascotas { get; set; } = null!;
    }
}
