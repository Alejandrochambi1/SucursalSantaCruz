using System;

namespace Sucursal.Core.DTOs
{
    /// <summary>
    /// Comando para REGISTRAR el reporte diario consolidado
    /// Se recibe como POST desde cada microservicio (Ventas, RRHH, Almacén, etc.)
    /// </summary>
    public class RegistrarReporteDiarioCommand
    {
        public string CodigoSucursal { get; set; }
        public DateTime Fecha { get; set; }
        
        // Datos de Ventas
        public decimal TotalVentasProductos { get; set; }
        public decimal TotalVentasServicios { get; set; }
        
        // Datos de RRHH
        public decimal CostoLaboralDia { get; set; }
        public int CantidadEmpleadosPresentes { get; set; }
        public int CantidadEmpleadosAusentes { get; set; }
        
        // Datos de Almacén
        public decimal StockValorado { get; set; }
        public int ProductosVencidos { get; set; }
        public int ProductosDevueltos { get; set; }
        
        // Datos operacionales
        public int CantidadTransacciones { get; set; }
        public int CantidadProductosVendidos { get; set; }
        
        // JSON completo con todos los detalles
        public string DetallesJSON { get; set; }
    }
}
