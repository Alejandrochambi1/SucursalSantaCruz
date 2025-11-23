using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucursal.Core.Entidades
{
    /// <summary>
    /// Items dentro de una solicitud de reposición
    /// Ej: 100 bolsas de Leche Deslactosada, 50 Quesos, etc.
    /// </summary>
    public class ItemSolicitud
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [ForeignKey(nameof(SolicitudInterna))]
        public Guid IdSolicitud { get; set; }
        
        public string CodigoArticulo { get; set; } // De Almacén de Fábrica
        public string NombreArticulo { get; set; }
        
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get => Cantidad * PrecioUnitario; }
        
        public SolicitudInterna Solicitud { get; set; }
    }
}
