using Sucursal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    /// <summary>
    /// CLIENTE HTTP PARA CONTABILIDAD
    /// Consumir: Presupuestos disponibles, gastos, estados financieros
    /// Esta es una dependencia que Sucursal necesita para validar si hay presupuesto antes de aprobar solicitudes
    /// </summary>
    public class ContabilidadClient : IContabilidadClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ContabilidadClient> _logger;

        public ContabilidadClient(HttpClient httpClient, ILogger<ContabilidadClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// GET /api/contabilidad/presupuesto/{codigoSucursal}/{centro}
        /// Traer presupuesto disponible de un centro de costo
        /// </summary>
        public async Task<dynamic> GetPresupuesto(string codigoSucursal, string centro)
        {
            try
            {
                _logger.LogInformation($"[CONTABILIDAD] Pidiendo presupuesto para sucursal {codigoSucursal}, centro {centro}");
                var response = await _httpClient.GetAsync($"/api/contabilidad/presupuesto/{codigoSucursal}/{centro}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[CONTABILIDAD] Presupuesto obtenido: {content}");
                    return content;
                }
                else
                {
                    _logger.LogError($"[CONTABILIDAD] Error {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[CONTABILIDAD] Excepción: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// GET /api/contabilidad/gastos/{codigoSucursal}/{fecha}
        /// Traer gastos del día/período para una sucursal
        /// </summary>
        public async Task<dynamic> GetGastos(string codigoSucursal, DateTime fecha)
        {
            try
            {
                _logger.LogInformation($"[CONTABILIDAD] Pidiendo gastos para sucursal {codigoSucursal}, fecha {fecha:yyyy-MM-dd}");
                var fechaStr = fecha.ToString("yyyy-MM-dd");
                var response = await _httpClient.GetAsync($"/api/contabilidad/gastos/{codigoSucursal}/{fechaStr}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[CONTABILIDAD] Gastos obtenidos: {content}");
                    return content;
                }
                else
                {
                    _logger.LogError($"[CONTABILIDAD] Error {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[CONTABILIDAD] Excepción: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// GET /api/contabilidad/estados-financieros/{codigoSucursal}/{periodo}
        /// Traer estados financieros (mensual, trimestral, anual)
        /// </summary>
        public async Task<dynamic> GetEstadosFinancieros(string codigoSucursal, string periodo)
        {
            try
            {
                _logger.LogInformation($"[CONTABILIDAD] Pidiendo estados financieros para sucursal {codigoSucursal}, período {periodo}");
                var response = await _httpClient.GetAsync($"/api/contabilidad/estados-financieros/{codigoSucursal}/{periodo}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[CONTABILIDAD] Estados financieros obtenidos");
                    return content;
                }
                else
                {
                    _logger.LogError($"[CONTABILIDAD] Error {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[CONTABILIDAD] Excepción: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// GET /api/contabilidad/presupuesto-disponible/{codigoSucursal}
        /// Traer presupuesto disponible total de la sucursal (para el dashboard)
        /// </summary>
        public async Task<dynamic> GetPresupuestoDisponible(string codigoSucursal)
        {
            try
            {
                _logger.LogInformation($"[CONTABILIDAD] Pidiendo presupuesto disponible para sucursal {codigoSucursal}");
                var response = await _httpClient.GetAsync($"/api/contabilidad/presupuesto-disponible/{codigoSucursal}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[CONTABILIDAD] Presupuesto disponible: {content}");
                    return content;
                }
                else
                {
                    _logger.LogError($"[CONTABILIDAD] Error {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[CONTABILIDAD] Excepción: {ex.Message}");
                return null;
            }
        }
    }
}
