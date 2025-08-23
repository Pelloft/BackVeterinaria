
using Microsoft.EntityFrameworkCore;
using VeterinariaApp.Domain.Models;


public class VeterinariaDbContext : DbContext
{
    public VeterinariaDbContext(DbContextOptions<VeterinariaDbContext> options)
        : base(options){}

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<DetalleFactura> Detalles { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Owner>()
            .HasMany(o => o.Mascotas)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Servicio>()
            .Property(s => s.Tarifa)
            .HasPrecision(10, 2);

        modelBuilder.Entity<DetalleFactura>()
            .Property(df => df.Subtotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Factura>()
        .Property(f => f.Subtotal)
        .HasPrecision(18, 2);

        modelBuilder.Entity<Factura>()
            .Property(f => f.IVA)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Factura>()
        .Property(f => f.Total)
        .HasPrecision(18, 2);

    }
}

