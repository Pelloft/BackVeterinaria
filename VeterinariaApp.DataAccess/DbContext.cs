
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
    }
}

