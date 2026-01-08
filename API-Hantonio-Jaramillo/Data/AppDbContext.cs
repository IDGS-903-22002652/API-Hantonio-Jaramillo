using API_Hantonio_Jaramillo.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Sucursal> Sucursales => Set<Sucursal>();
    public DbSet<CatEstatus> CatEstatus => Set<CatEstatus>();
    public DbSet<CatTipoTraje> CatTipoTrajes => Set<CatTipoTraje>();
    public DbSet<CatRecursosDiseno> CatRecursosDiseno => Set<CatRecursosDiseno>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<LogAcceso> LogAccesos => Set<LogAcceso>();
    public DbSet<Orden> Ordenes => Set<Orden>();
    public DbSet<MedidasOrden> MedidasOrdenes => Set<MedidasOrden>();
    public DbSet<DetalleSaco> DetalleSacos => Set<DetalleSaco>();
    public DbSet<DetalleCamisa> DetalleCamisas => Set<DetalleCamisa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Índice único para login
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Login)
            .IsUnique();

        // Relación uno a uno MedidasOrden - Orden
        modelBuilder.Entity<MedidasOrden>()
            .HasIndex(m => m.IdOrden)
            .IsUnique();

        // Precisión para decimales en MedidasOrden
        modelBuilder.Entity<MedidasOrden>(entity =>
        {
            entity.Property(m => m.CCuello).HasPrecision(5, 2);
            entity.Property(m => m.CManga).HasPrecision(5, 2);
            entity.Property(m => m.PCadera).HasPrecision(5, 2);
            entity.Property(m => m.PCintura).HasPrecision(5, 2);
            entity.Property(m => m.PLargo).HasPrecision(5, 2);
            entity.Property(m => m.PTiro).HasPrecision(5, 2);
            entity.Property(m => m.SEstomago).HasPrecision(5, 2);
            entity.Property(m => m.SHombros).HasPrecision(5, 2);
            entity.Property(m => m.SLargoFrente).HasPrecision(5, 2);
            entity.Property(m => m.SPecho).HasPrecision(5, 2);
        });

        // Precisión para decimales en Orden
        modelBuilder.Entity<Orden>(entity =>
        {
            entity.Property(o => o.CostoTotal).HasPrecision(10, 2);
            entity.Property(o => o.MontoAbonado).HasPrecision(10, 2);
        });
    }
}