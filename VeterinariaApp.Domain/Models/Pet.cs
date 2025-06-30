

using System.ComponentModel.DataAnnotations;

namespace VeterinariaApp.Domain.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La especie es obligatoria.")]
        public string Especie { get; set; } = null!;
        public string Raza { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }


        //FK

        [Required(ErrorMessage = "Debe asignarse un dueño.")]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; } = null!;
    }
}
