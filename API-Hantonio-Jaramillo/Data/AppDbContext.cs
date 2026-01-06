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
    }
}