

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApp.Domain.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        //public string NumeroFactura { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Cliente { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Mascota {  get; set; } = string.Empty;
        
        [Required]
        public DateTime Fecha { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal IVA { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }


        public List<DetalleFactura> Detalles { get; set; } = new();
    }
}
