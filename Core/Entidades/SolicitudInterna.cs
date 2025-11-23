using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucursal.Core.Entidades
{
    /// <summary>
    /// Solicitudes CREADAS en Sucursal (Reposición, Marketing, Permisos, etc.)
    /// Se envían a otros servicios para aprobación o ejecución
    /// </summary>
    public class SolicitudInterna
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [ForeignKey(nameof(SucursalEntidad))]
        public string CodigoSucursal { get; set; }
        
        public enum TipoSolicitud { Reposicion, Marketing, Soporte, Permiso }
        public TipoSolicitud Tipo { get; set; }
        
        public enum EstadoSolicitud { Pendiente, Aprobada, Rechazada, EnProgreso, Completada }
        public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Pendiente;
        
        public string Descripcion { get; set; }
        
        // Para Marketing: monto de presupuesto solicitado
        // Para Reposición: se calcula de los items
        public decimal? MontoSolicitado { get; set; }
        
        public string CISolicitante { get; set; } // CI del que pidió
        public string CIAprobador { get; set; } // CI del gerente que aprobó (Alejandro)
        
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaCompletacion { get; set; }
        
        public string MotivoRechazo { get; set; }
        
        // JSON adicional para almacenar datos específicos
        public string DetallesJSON { get; set; }
        
        public SucursalEntidad Sucursal { get; set; }
        public ICollection<ItemSolicitud> Items { get; set; } = new List<ItemSolicitud>();
    }
}
