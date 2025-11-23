using Microsoft.EntityFrameworkCore;
using Sucursal.Core.Entidades;

namespace Sucursal.Infraestructura.Data
{
    /// <summary>
    /// DbContext para la base de datos de Sucursal Santa Cruz
    /// </summary>
    public class SucursalDbContext : DbContext
    {
        public SucursalDbContext(DbContextOptions<SucursalDbContext> options) : base(options) { }

        public DbSet<SucursalEntidad> Sucursales { get; set; }
        public DbSet<GerenteSucursal> GerentesSucursal { get; set; }
        public DbSet<SolicitudInterna> SolicitudesInternas { get; set; }
        public DbSet<ItemSolicitud> ItemsSolicitud { get; set; }
        public DbSet<ReporteDiarioConsolidado> ReportesDiarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones
            modelBuilder.Entity<GerenteSucursal>()
                .HasOne(g => g.Sucursal)
                .WithMany()
                .HasForeignKey(g => g.CodigoSucursal);

            modelBuilder.Entity<SolicitudInterna>()
                .HasOne(s => s.Sucursal)
                .WithMany()
                .HasForeignKey(s => s.CodigoSucursal);

            modelBuilder.Entity<ItemSolicitud>()
                .HasOne(i => i.Solicitud)
                .WithMany(s => s.Items)
                .HasForeignKey(i => i.IdSolicitud)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReporteDiarioConsolidado>()
                .HasOne(r => r.Sucursal)
                .WithMany()
                .HasForeignKey(r => r.CodigoSucursal);

            // Crear índices
            modelBuilder.Entity<SolicitudInterna>()
                .HasIndex(s => new { s.CodigoSucursal, s.Estado });

            modelBuilder.Entity<ReporteDiarioConsolidado>()
                .HasIndex(r => new { r.CodigoSucursal, r.Fecha });
        }
    }
}
