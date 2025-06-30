


namespace VeterinariaApp.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;


        public int IntentosFallidos { get; set; } = 0;
        public DateTime? Bloqueado { get; set; }

    }
}
