using Sucursal.Core.DTOs;
using Sucursal.Core.Entidades;
using Sucursal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Servicios
{
    /// <summary>
    /// Servicio para gestionar REPORTES CONSOLIDADOS DIARIOS
    /// Recibe reportes de TODOS los microservicios (Ventas, RRHH, Almacén, etc.)
    /// </summary>
    public class ReportesService
    {
        private readonly IReporteRepository _reporteRepository;
        private readonly IVentasClient _ventasClient;
        private readonly IAlmacenClient _almacenClient;
        private readonly IRRHHClient _rrhhClient;
        private readonly IContabilidadClient _contabilidadClient;
        
        public ReportesService(
            IReporteRepository reporteRepository,
            IVentasClient ventasClient,
            IAlmacenClient almacenClient,
            IRRHHClient rrhhClient,
            IContabilidadClient contabilidadClient)
        {
            _reporteRepository = reporteRepository;
            _ventasClient = ventasClient;
            _almacenClient = almacenClient;
            _rrhhClient = rrhhClient;
            _contabilidadClient = contabilidadClient;
        }
        
        /// <summary>
        /// REGISTRAR REPORTE DIARIO
        /// Recibe los datos consolidados de TODOS los servicios
        /// </summary>
        public async Task<Guid> RegistrarReporteDiario(RegistrarReporteDiarioCommand command)
        {
            var reporte = new ReporteDiarioConsolidado
            {
                CodigoSucursal = command.CodigoSucursal,
                Fecha = command.Fecha,
                
                // VENTAS
                TotalVentasProductos = command.TotalVentasProductos,
                TotalVentasServicios = command.TotalVentasServicios,
                
                // RRHH
                CostoLaboralDia = command.CostoLaboralDia,
                CantidadEmpleadosPresentes = command.CantidadEmpleadosPresentes,
                CantidadEmpleadosAusentes = command.CantidadEmpleadosAusentes,
                
                // ALMACÉN
                StockValorado = command.StockValorado,
                ProductosVencidos = command.ProductosVencidos,
                ProductosDevueltos = command.ProductosDevueltos,
                
                // OPERACIONAL
                CantidadTransacciones = command.CantidadTransacciones,
                CantidadProductosVendidos = command.CantidadProductosVendidos,
                
                // JSON completo
                DetallesJSON = command.DetallesJSON,
                EstadoConsolidacion = "Completo",
                FechaConsolidacion = DateTime.UtcNow
            };
            
            // Guardar en base de datos
            return await _reporteRepository.Add(reporte);
        }
        
        /// <summary>
        /// OBTENER REPORTE DIARIO
        /// </summary>
        public async Task<ReporteDiarioConsolidado> ObtenerReporteDiario(string codigoSucursal, DateTime fecha)
        {
            return await _reporteRepository.GetDiario(codigoSucursal, fecha);
        }
        
        /// <summary>
        /// GENERAR CONSOLIDADO MENSUAL
        /// Suma todos los reportes diarios del mes
        /// </summary>
        public async Task<dynamic> GenerarReporteMensual(string codigoSucursal, int mes, int anio)
        {
            var reportesDia = await _reporteRepository.GetMensual(codigoSucursal, mes, anio);
            
            if (!reportesDia.Any())
                return null;
            
            var consolidado = new
            {
                codigoSucursal,
                mes,
                anio,
                totalDias = reportesDia.Count,
                
                // Sumatoria de ingresos
                totalVentasProductos = reportesDia.Sum(r => r.TotalVentasProductos),
                totalVentasServicios = reportesDia.Sum(r => r.TotalVentasServicios),
                totalIngresos = reportesDia.Sum(r => r.TotalIngresos),
                
                // Sumatoria de egresos
                costoLaboralMensual = reportesDia.Sum(r => r.CostoLaboralDia),
                costoOperativoMensual = reportesDia.Sum(r => r.CostoOperativoDia),
                costoInventarioMensual = reportesDia.Sum(r => r.CostoInventarioDia),
                totalEgresos = reportesDia.Sum(r => r.TotalEgresos),
                
                // Resultado
                utilidadNetaMensual = reportesDia.Sum(r => r.UtilidadBruta),
                
                // Promedio por día
                promedioVentasDia = reportesDia.Average(r => r.TotalVentasProductos + r.TotalVentasServicios),
                promedioUtilidadDia = reportesDia.Average(r => r.UtilidadBruta)
            };
            
            return consolidado;
        }
    }
}
