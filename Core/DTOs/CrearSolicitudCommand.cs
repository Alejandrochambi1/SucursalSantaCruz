using System;
using System.Collections.Generic;

namespace Sucursal.Core.DTOs
{
    /// <summary>
    /// Comando para CREAR una nueva solicitud
    /// </summary>
    public class CrearSolicitudCommand
    {
        public string CodigoSucursal { get; set; } // "SCZ001"
        
        public string Tipo { get; set; } // "Reposicion" o "Marketing"
        
        public string Descripcion { get; set; }
        
        public decimal? MontoSolicitado { get; set; } // Para Marketing
        
        public string CISolicitante { get; set; } // CI de quien solicita
        
        public List<ItemSolicitudDTO> Items { get; set; } = new();
    }
    
    public class ItemSolicitudDTO
    {
        public string CodigoArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
