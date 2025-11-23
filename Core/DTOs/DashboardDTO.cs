using System;
using System.Collections.Generic;

namespace Sucursal.Core.DTOs
{
    /// <summary>
    /// DTO para el Dashboard del Gerente (Alejandro)
    /// Muestra el "estado del día" de forma consolidada
    /// </summary>
    public class DashboardDTO
    {
        public string CodigoSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public DateTime Fecha { get; set; }
        
        // Monitor de Signos Vitales
        public decimal TotalVentas { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal UtilidadNeta { get; set; }
        public decimal PorcentajeUtilidad { get; set; }
        
        // Presupuesto
        public decimal PresupuestoDisponible { get; set; }
        public decimal PresupuestoUtilizado { get; set; }
        public decimal PorcentajeUtilizacion { get; set; }
        
        // Alertas críticas
        public List<AlertaDTO> Alertas { get; set; } = new();
        
        // Productos en riesgo
        public List<ProductoCriticoDTO> ProductosCriticos { get; set; } = new();
        
        // Solicitudes pendientes
        public int SolicitudesPendientes { get; set; }
        public List<SolicitudResumenDTO> Ultimas5Solicitudes { get; set; } = new();
        
        // Personal
        public int EmpleadosPresentes { get; set; }
        public int EmpleadosAusentes { get; set; }
        
        // Última consolidación
        public DateTime? UltimaConsolidacion { get; set; }
    }
    
    public class AlertaDTO
    {
        public string TipoAlerta { get; set; } // "StockBajo", "PresupuestoAlerta", "Error", etc.
        public string Prioridad { get; set; } // "Baja", "Media", "Alta", "Crítica"
        public string Descripcion { get; set; }
        public DateTime FechaAlerta { get; set; }
    }
    
    public class ProductoCriticoDTO
    {
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public string Motivo { get; set; } // "Bajo", "Vencido", "Devuelto"
    }
    
    public class SolicitudResumenDTO
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal? Monto { get; set; }
    }
}
