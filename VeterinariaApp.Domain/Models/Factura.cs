

using System.ComponentModel.DataAnnotations;

namespace VeterinariaApp.Domain.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }
        //public string NumeroFactura { get; set; } = string.Empty;
        [Required]
        public string Cliente { get; set; } = string.Empty;
        [Required]
        public string Mascota {  get; set; } = string.Empty;
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public decimal Subtotal { get; set; }
        [Required]
        public decimal IVA { get; set; }
        [Required]
        public decimal Total { get; set; }


        public List<DetalleFactura> Detalles { get; set; } = new();
    }
}
