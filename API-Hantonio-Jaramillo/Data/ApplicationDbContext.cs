using API_Hantonio_Jaramillo.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<EstatusOrden> EstatusOrdenes { get; set; }
        public DbSet<TipoTraje> TiposTraje { get; set; }
        public DbSet<MedidasOrden> MedidasOrdenes { get; set; }
        public DbSet<DetalleSaco> DetalleSacos { get; set; }
        public DbSet<DetallePantalon> DetallePantalones { get; set; }
        public DbSet<DetalleChaleco> DetalleChalecos { get; set; }
        public DbSet<DetalleCamisa> DetalleCamisas { get; set; }
        public DbSet<DetalleZapato> DetalleZapatos { get; set; }
        public DbSet<Finanza> Finanzas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- 1. SOLUCIÓN AL ERROR DE RELACIÓN (Sucursal <-> Usuario) ---
            // Le decimos que una Sucursal tiene UN Usuario encargado, 
            // pero ese Usuario NO es el mismo que la lista de empleados.
            modelBuilder.Entity<Sucursal>()
                .HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación de empleados de la sucursal
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Sucursal)
                .WithMany(s => s.Usuarios)
                .HasForeignKey(u => u.IdSucursal)
                .OnDelete(DeleteBehavior.Restrict);

            // --- 2. CONFIGURACIÓN DE DECIMALES ---
            // --- 2. CONFIGURACIÓN DE DECIMALES ---
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            // --- AGREGAR ESTO: CONFIGURACIÓN DE BORRADO EN CASCADA PARA ÓRDENES ---
            // Aseguramos que si se elimina una Orden, se eliminen sus detalles y medidas
            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Medidas)
                .WithOne() // Asumiendo relación 1 a 1. Si es 1 a N, usa WithMany()
                .HasForeignKey<MedidasOrden>(m => m.IdOrden)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orden>()
                .HasOne(o => o.DetalleSaco)
                .WithOne()
                .HasForeignKey<DetalleSaco>(d => d.IdOrden)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orden>()
                .HasOne(o => o.DetallePantalon)
                .WithOne()
                .HasForeignKey<DetallePantalon>(d => d.IdOrden)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orden>()
                .HasOne(o => o.DetalleChaleco)
                .WithOne()
                .HasForeignKey<DetalleChaleco>(d => d.IdOrden)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orden>()
                .HasOne(o => o.DetalleCamisa)
                .WithOne()
                .HasForeignKey<DetalleCamisa>(d => d.IdOrden)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orden>()
                .HasOne(o => o.DetalleZapato)
                .WithOne()
                .HasForeignKey<DetalleZapato>(d => d.IdOrden)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rol>().HasData(
                new Rol { IdRol = 1, Nombre = "Administrador" },
                new Rol { IdRol = 2, Nombre = "Empleado" }
            );

            modelBuilder.Entity<EstatusOrden>().HasData(
                new EstatusOrden { IdEstatus = 1, Descripcion = "Nueva orden" },
                new EstatusOrden { IdEstatus = 2, Descripcion = "En revisión" },
                new EstatusOrden { IdEstatus = 3, Descripcion = "Pendiente de medidas" },
                new EstatusOrden { IdEstatus = 4, Descripcion = "Medidas registradas" },
                new EstatusOrden { IdEstatus = 5, Descripcion = "Autorizado para producción" },
                new EstatusOrden { IdEstatus = 6, Descripcion = "En confección" },
                new EstatusOrden { IdEstatus = 7, Descripcion = "Listo para entrega" },
                new EstatusOrden { IdEstatus = 8, Descripcion = "Entregado" },
                new EstatusOrden { IdEstatus = 9, Descripcion = "Cancelado" }
            );

            modelBuilder.Entity<TipoTraje>().HasData(
                new TipoTraje { IdTipoTraje = 1, Descripcion = "Dos piezas" },
                new TipoTraje { IdTipoTraje = 2, Descripcion = "Tres piezas" },
                new TipoTraje { IdTipoTraje = 3, Descripcion = "Saco" },
                new TipoTraje { IdTipoTraje = 4, Descripcion = "Pantalón" },
                new TipoTraje { IdTipoTraje = 5, Descripcion = "Chaleco" },
                new TipoTraje { IdTipoTraje = 6, Descripcion = "Camisa" },
                new TipoTraje { IdTipoTraje = 7, Descripcion = "Frac" },
                new TipoTraje { IdTipoTraje = 8, Descripcion = "Chaque" },
                new TipoTraje { IdTipoTraje = 9, Descripcion = "Smoking" },
                new TipoTraje { IdTipoTraje = 10, Descripcion = "Zapatos" }




            );

            modelBuilder.Entity<Sucursal>().HasData(
                new Sucursal
                {
                    IdSucursal = 1,
                    Nombre = "Sucursal Jardines del Moral",
                    Direccion = "C. del Fuego 226 A, Jardines del Moral, 37160 León de los Aldama, Gto.",
                    Telefono = "477 799 3177",
                    Estatus = true
                }
            );
        }
    }
}