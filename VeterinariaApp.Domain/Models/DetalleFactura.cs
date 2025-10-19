

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApp.Domain.Models
{
    public class DetalleFactura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ServicioId { get; set; } // FK a Servicios

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        // Relación con Factura
        [Required]
        public int FacturaId { get; set; }

        [ForeignKey("FacturaId")]
        public Factura? Factura { get; set; }
    }
}
