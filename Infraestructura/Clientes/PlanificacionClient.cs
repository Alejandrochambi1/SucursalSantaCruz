using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class PlanificacionClient : IPlanificacionClient
    {
        private readonly HttpClient _httpClient;

        public PlanificacionClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetProyeccionesDemanda(string codigoSucursal, string periodo)
        {
            try
            {
                var url = $"/api/demanda/proyecciones?sucursal={codigoSucursal}&periodo={periodo}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Planificación: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetPlanProduccion(string codigoSucursal, string mes)
        {
            try
            {
                var url = $"/api/plan/produccion?sucursal={codigoSucursal}&mes={mes}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Planificación: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetForecast(string codigoSucursal)
        {
            try
            {
                var url = $"/api/forecast?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Planificación: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetReportePlanificacion(string codigoSucursal, string trimestre)
        {
            try
            {
                var url = $"/api/reportes/planificacion?sucursal={codigoSucursal}&trimestre={trimestre}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Planificación: {ex.Message}");
                return null;
            }
        }
    }
}
