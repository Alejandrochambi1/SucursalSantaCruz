using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucursal.Core.Entidades
{
    /// <summary>
    /// Entidad que representa el Gerente de una sucursal (Alejandro en nuestro caso)
    /// </summary>
    public class GerenteSucursal
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [ForeignKey(nameof(SucursalEntidad))]
        public string CodigoSucursal { get; set; }
        
        public string CI { get; set; } // Cédula del gerente
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        
        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Activo";
        
        public SucursalEntidad Sucursal { get; set; }
    }
}
