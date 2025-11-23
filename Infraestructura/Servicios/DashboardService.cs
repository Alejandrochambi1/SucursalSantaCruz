using Sucursal.Core.DTOs;
using Sucursal.Core.Interfaces;
using Sucursal.Core.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Servicios
{
    /// <summary>
    /// Servicio de aplicación que ORQUESTA la construcción del Dashboard
    /// Llama a TODOS los microservicios para traer datos consolidados
    /// Este es el CEREBRO de Sucursal
    /// </summary>
    public class DashboardService
    {
        private readonly IVentasClient _ventasClient;
        private readonly IAlmacenClient _almacenClient;
        private readonly IRRHHClient _rrhhClient;
        private readonly IContabilidadClient _contabilidadClient;
        private readonly IMarketingClient _marketingClient;
        private readonly IReporteRepository _reporteRepository;
        private readonly ISolicitudRepository _solicitudRepository;
        
        public DashboardService(
            IVentasClient ventasClient,
            IAlmacenClient almacenClient,
            IRRHHClient rrhhClient,
            IContabilidadClient contabilidadClient,
            IMarketingClient marketingClient,
            IReporteRepository reporteRepository,
            ISolicitudRepository solicitudRepository)
        {
            _ventasClient = ventasClient;
            _almacenClient = almacenClient;
            _rrhhClient = rrhhClient;
            _contabilidadClient = contabilidadClient;
            _marketingClient = marketingClient;
            _reporteRepository = reporteRepository;
            _solicitudRepository = solicitudRepository;
        }
        
        /// <summary>
        /// GENERA EL DASHBOARD: Consulta TODOS los microservicios para traer datos consolidados
        /// Esto es lo que ve Alejandro en su pantalla
        /// </summary>
        public async Task<DashboardDTO> ObtenerDashboard(string codigoSucursal)
        {
            var hoy = DateTime.UtcNow;
            var dashboard = new DashboardDTO
            {
                CodigoSucursal = codigoSucursal,
                NombreSucursal = "Sucursal Santa Cruz",
                Fecha = hoy
            };
            
            try
            {
                // 1. VENTAS: Traer total de ventas del día
                var ventasData = await _ventasClient.GetReporteDiario(codigoSucursal, hoy);
                if (ventasData != null)
                {
                    dashboard.TotalVentas = ventasData["totalIngresos"] ?? 0m;
                }
                
                // 2. ALMACÉN: Traer stock crítico, productos vencidos, devueltos
                var almacenData = await _almacenClient.GetInventario(codigoSucursal);
                if (almacenData != null)
                {
                    var productos = almacenData as List<dynamic> ?? new List<dynamic>();
                    
                    // Buscar productos con stock bajo
                    foreach (var prod in productos)
                    {
                        if (prod["stock"] < prod["stockMinimo"])
                        {
                            dashboard.ProductosCriticos.Add(new ProductoCriticoDTO
                            {
                                CodigoProducto = prod["codigo"],
                                NombreProducto = prod["nombre"],
                                StockActual = prod["stock"],
                                StockMinimo = prod["stockMinimo"],
                                Motivo = "Bajo"
                            });
                        }
                    }
                }
                
                // 3. RRHH: Traer asistencia y costo laboral del día
                var rrhhData = await _rrhhClient.GetAsistencia(codigoSucursal, hoy);
                if (rrhhData != null)
                {
                    dashboard.EmpleadosPresentes = rrhhData["presentes"] ?? 0;
                    dashboard.EmpleadosAusentes = rrhhData["ausentes"] ?? 0;
                }
                
                // 4. CONTABILIDAD: Traer presupuesto disponible
                var contabData = await _contabilidadClient.GetPresupuestoDisponible(codigoSucursal);
                if (contabData != null)
                {
                    dashboard.PresupuestoDisponible = contabData["disponible"] ?? 0m;
                }
                
                // 5. MARKETING: Traer campañas activas
                var marketingData = await _marketingClient.GetCampanasActivas(codigoSucursal);
                if (marketingData != null)
                {
                    // Procesar datos de marketing
                }
                
                // 6. Solicitudes pendientes en Sucursal
                var solicitudesPendientes = await _solicitudRepository.GetPendientes(codigoSucursal);
                dashboard.SolicitudesPendientes = solicitudesPendientes.Count;
                dashboard.Ultimas5Solicitudes = solicitudesPendientes
                    .Take(5)
                    .Select(s => s.ToResumenDTO())
                    .ToList();
                
                // 7. Generar ALERTAS automáticas
                dashboard.Alertas = GenerarAlertas(dashboard);
                
                // 8. Calcular utilidad neta
                dashboard.TotalGastos = dashboard.EmpleadosPresentes * 150; // Estimación simple
                dashboard.UtilidadNeta = dashboard.TotalVentas - dashboard.TotalGastos;
                dashboard.PorcentajeUtilidad = dashboard.TotalVentas > 0 
                    ? (dashboard.UtilidadNeta / dashboard.TotalVentas) * 100 
                    : 0;
                
                // 9. Obtener última consolidación
                var ultimoReporte = await _reporteRepository.GetDiario(codigoSucursal, hoy);
                dashboard.UltimaConsolidacion = ultimoReporte?.FechaConsolidacion;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error construyendo dashboard: {ex.Message}");
            }
            
            return dashboard;
        }
        
        /// <summary>
        /// Genera ALERTAS automáticas basadas en los datos del dashboard
        /// Ej: "Stock de Leche Deslactosada al 10%"
        /// </summary>
        private List<AlertaDTO> GenerarAlertas(DashboardDTO dashboard)
        {
            var alertas = new List<AlertaDTO>();
            
            // Alerta de productos críticos
            if (dashboard.ProductosCriticos.Count > 0)
            {
                alertas.Add(new AlertaDTO
                {
                    TipoAlerta = "StockBajo",
                    Prioridad = "Alta",
                    Descripcion = $"{dashboard.ProductosCriticos.Count} productos con stock bajo",
                    FechaAlerta = DateTime.UtcNow
                });
            }
            
            // Alerta de presupuesto
            if (dashboard.PresupuestoDisponible < 1000)
            {
                alertas.Add(new AlertaDTO
                {
                    TipoAlerta = "PresupuestoAlerta",
                    Prioridad = "Media",
                    Descripcion = $"Presupuesto bajo: ${dashboard.PresupuestoDisponible}",
                    FechaAlerta = DateTime.UtcNow
                });
            }
            
            // Alerta de ausencias
            if (dashboard.EmpleadosAusentes > 2)
            {
                alertas.Add(new AlertaDTO
                {
                    TipoAlerta = "RecursoHumano",
                    Prioridad = "Media",
                    Descripcion = $"{dashboard.EmpleadosAusentes} empleados ausentes",
                    FechaAlerta = DateTime.UtcNow
                });
            }
            
            // Alerta de utilidad baja
            if (dashboard.PorcentajeUtilidad < 10)
            {
                alertas.Add(new AlertaDTO
                {
                    TipoAlerta = "Rentabilidad",
                    Prioridad = "Alta",
                    Descripcion = $"Margen de ganancia bajo: {dashboard.PorcentajeUtilidad:F2}%",
                    FechaAlerta = DateTime.UtcNow
                });
            }
            
            return alertas;
        }
    }
}
