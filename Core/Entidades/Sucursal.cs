using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sucursal.Core.Entidades
{
    /// <summary>
    /// Entidad que representa una sucursal (solo metadata).
    /// NO almacena ventas, inventario, empleados (esos son de otros microservicios)
    /// La sucursal es el CEREBRO ORQUESTADOR que consulta todo
    /// </summary>
    public class SucursalEntidad
    {
        [Key]
        public string CodigoSucursal { get; set; } // "SCZ001" para Santa Cruz
        
        public string Nombre { get; set; } // "Sucursal Santa Cruz"
        public string Ciudad { get; set; } // "Santa Cruz"
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        
        [Range(0, 23)]
        public int HoraApertura { get; set; } // 8 (08:00)
        
        [Range(0, 23)]
        public int HoraCierre { get; set; } // 20 (20:00)
        
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Activo"; // Activo, Inactivo
    }
}
