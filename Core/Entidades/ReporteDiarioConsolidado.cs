using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucursal.Core.Entidades
{
    /// <summary>
    /// Reporte consolidado DIARIO que recibe Sucursal de TODOS los roles
    /// Se genera al final del día (18:00 = cierre)
    /// </summary>
    public class ReporteDiarioConsolidado
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [ForeignKey(nameof(SucursalEntidad))]
        public string CodigoSucursal { get; set; }
        
        public DateTime Fecha { get; set; }
        
        // INGRESOS (de Ventas)
        public decimal TotalVentasProductos { get; set; }
        public decimal TotalVentasServicios { get; set; }
        public decimal TotalIngresos => TotalVentasProductos + TotalVentasServicios;
        
        // EGRESOS (de todos los servicios)
        public decimal CostoLaboralDia { get; set; } // De RRHH
        public decimal CostoOperativoDia { get; set; } // Servicios, mantención
        public decimal CostoInventarioDia { get; set; } // De Almacén
        public decimal CostoMarketingDia { get; set; } // De Marketing
        public decimal TotalEgresos => CostoLaboralDia + CostoOperativoDia + CostoInventarioDia + CostoMarketingDia;
        
        // RESULTADO
        public decimal UtilidadBruta => TotalIngresos - TotalEgresos;
        public decimal PorcentajeUtilidadBruta { get; set; }
        
        // INVENTARIO (valorado en pesos)
        public decimal StockValorado { get; set; } // De Almacén
        public int ProductosVencidos { get; set; } // De Almacén
        public int ProductosDevueltos { get; set; } // De Almacén
        
        // OPERACIONAL
        public int CantidadTransacciones { get; set; }
        public int CantidadProductosVendidos { get; set; }
        public int CantidadClientes { get; set; }
        
        // PERSONAL (de RRHH)
        public int CantidadEmpleadosPresentes { get; set; }
        public int CantidadEmpleadosAusentes { get; set; }
        public decimal NominaDelDia { get; set; }
        
        // MARKETING
        public decimal PresupuestoGastadoMarketing { get; set; }
        public int CampanasActivas { get; set; }
        
        // ESTADO
        public string EstadoConsolidacion { get; set; } = "Pendiente"; // Pendiente, Completo, Parcial
        public DateTime? FechaConsolidacion { get; set; }
        
        // JSON con detalles completos de cada servicio
        public string DetallesJSON { get; set; }
        
        public SucursalEntidad Sucursal { get; set; }
    }
}
