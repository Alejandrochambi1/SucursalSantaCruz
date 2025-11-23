using Microsoft.AspNetCore.Mvc;
using Sucursal.Core.DTOs;
using Sucursal.Infraestructura.Servicios;
using System;
using System.Threading.Tasks;

namespace Sucursal.Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalController : ControllerBase
    {
        private readonly DashboardService _dashboardService;
        private readonly SolicitudesService _solicitudesService;
        private readonly ReportesService _reportesService;

        public SucursalController(
            DashboardService dashboardService,
            SolicitudesService solicitudesService,
            ReportesService reportesService)
        {
            _dashboardService = dashboardService;
            _solicitudesService = solicitudesService;
            _reportesService = reportesService;
        }

        /// <summary>
        /// GET DASHBOARD
        /// Devuelve la vista consolidada del día para Alejandro
        /// Consulta: Ventas, RRHH, Almacén, Contabilidad, Marketing, etc.
        /// </summary>
        [HttpGet("dashboard/{codigoSucursal}")]
        public async Task<IActionResult> GetDashboard(string codigoSucursal)
        {
            try
            {
                var dashboard = await _dashboardService.ObtenerDashboard(codigoSucursal);
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// POST CREAR SOLICITUD
        /// Crea una nueva solicitud de Reposición o Marketing
        /// </summary>
        [HttpPost("solicitudes")]
        public async Task<IActionResult> CrearSolicitud([FromBody] CrearSolicitudCommand command)
        {
            try
            {
                Guid idSolicitud;

                if (command.Tipo == "Reposicion")
                {
                    idSolicitud = await _solicitudesService.CrearSolicitudReposicion(command);
                }
                else if (command.Tipo == "Marketing")
                {
                    idSolicitud = await _solicitudesService.CrearSolicitudMarketing(command);
                }
                else
                {
                    return BadRequest("Tipo de solicitud no válido");
                }

                return Created($"/api/sucursal/solicitudes/{idSolicitud}", new { id = idSolicitud });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// PUT ACTUALIZAR ESTADO SOLICITUD
        /// Aprueba o rechaza una solicitud existente
        /// </summary>
        [HttpPut("solicitudes/{idSolicitud}")]
        public async Task<IActionResult> ActualizarEstadoSolicitud(
            Guid idSolicitud,
            [FromBody] ActualizarEstadoSolicitudCommand command)
        {
            try
            {
                command.IdSolicitud = idSolicitud;

                if (command.NuevoEstado == "Aprobada")
                {
                    var resultado = await _solicitudesService.AprobarSolicitud(
                        idSolicitud,
                        command.CIAprobador);

                    return Ok(new { mensaje = "Solicitud aprobada", resultado });
                }
                else if (command.NuevoEstado == "Rechazada")
                {
                    var resultado = await _solicitudesService.RechazarSolicitud(
                        idSolicitud,
                        command.CIAprobador,
                        command.MotivoRechazo);

                    return Ok(new { mensaje = "Solicitud rechazada", resultado });
                }
                else
                {
                    return BadRequest("Estado no válido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// GET SOLICITUDES PENDIENTES
        /// Devuelve todas las solicitudes esperando aprobación
        /// </summary>
        [HttpGet("solicitudes/pendientes/{codigoSucursal}")]
        public async Task<IActionResult> GetSolicitudesPendientes(string codigoSucursal)
        {
            try
            {
                var solicitudes = await _solicitudesService.ObtenerPendientes(codigoSucursal);
                return Ok(solicitudes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// POST REGISTRAR REPORTE DIARIO
        /// Recibe los reportes consolidados de todos los servicios
        /// Se llama al final del día (18:00)
        /// </summary>
        [HttpPost("reportes/diario")]
        public async Task<IActionResult> RegistrarReporteDiario([FromBody] RegistrarReporteDiarioCommand command)
        {
            try
            {
                var idReporte = await _reportesService.RegistrarReporteDiario(command);
                return Created($"/api/sucursal/reportes/{idReporte}", new { id = idReporte });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// GET REPORTE DIARIO
        /// Devuelve el reporte consolidado de un día específico
        /// </summary>
        [HttpGet("reportes/diario/{codigoSucursal}")]
        public async Task<IActionResult> GetReporteDiario(string codigoSucursal, [FromQuery] DateTime fecha)
        {
            try
            {
                var reporte = await _reportesService.ObtenerReporteDiario(codigoSucursal, fecha);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// GET REPORTE MENSUAL
        /// Devuelve el consolidado de un mes completo
        /// </summary>
        [HttpGet("reportes/mensual/{codigoSucursal}")]
        public async Task<IActionResult> GetReporteMensual(
            string codigoSucursal,
            [FromQuery] int mes,
            [FromQuery] int anio)
        {
            try
            {
                var reporte = await _reportesService.GenerarReporteMensual(codigoSucursal, mes, anio);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
