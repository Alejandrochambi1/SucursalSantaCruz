using Sucursal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    /// <summary>
    /// CLIENTE HTTP PARA MARKETING
    /// Consumir: Campañas activas, presupuestos de campañas, resultados
    /// Sucursal necesita ver qué campañas están gastando presupuesto para evaluarlas
    /// </summary>
    public class MarketingClient : IMarketingClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MarketingClient> _logger;

        public MarketingClient(HttpClient httpClient, ILogger<MarketingClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// GET /api/marketing/campanas-activas/{codigoSucursal}
        /// Traer todas las campañas activas en la sucursal
        /// </summary>
        public async Task<dynamic> GetCampanasActivas(string codigoSucursal)
        {
            try
            {
                _logger.LogInformation($"[MARKETING] Pidiendo campañas activas para sucursal {codigoSucursal}");
                var response = await _httpClient.GetAsync($"/api/marketing/campanas-activas/{codigoSucursal}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[MARKETING] Campañas activas obtenidas: {content}");
                    return content;
                }
                else
                {
                    _logger.LogError($"[MARKETING] Error {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MARKETING] Excepción: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// GET /api/marketing/presupuesto-campanas/{codigoSucursal}
        /// Traer presupuesto asignado vs. gastado en campañas
        /// </summary>
        public async Task<dynamic> GetPresupuestoCampanas(string codigoSucursal)
        {
            try
            {
                _logger.LogInformation($"[MARKETING] Pidiendo presupuesto de campañas para sucursal {codigoSucursal}");
                var response = await _httpClient.GetAsync($"/api/marketing/presupuesto-campanas/{codigoSucursal}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[MARKETING] Presupuesto de campañas obtenido");
                    return content;
                }
                else
                {
                    _logger.LogError($"[MARKETING] Error {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MARKETING] Excepción: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// GET /api/marketing/resultados-campana/{idCampana}
        /// Traer resultados de una campaña específica (clicks, conversiones, ROI)
        /// </summary>
        public async Task<dynamic> GetResultadosCampana(Guid idCampana)
        {
            try
            {
                _logger.LogInformation($"[MARKETING] Pidiendo resultados de campaña {idCampana}");
                var response = await _httpClient.GetAsync($"/api/marketing/resultados-campana/{idCampana}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[MARKETING] Resultados de campaña obtenidos");
                    return content;
                }
                else
                {
                    _logger.LogError($"[MARKETING] Error {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MARKETING] Excepción: {ex.Message}");
                return null;
            }
        }
    }
}

