using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class LogisticaClient : ILogisticaClient
    {
        private readonly HttpClient _httpClient;

        public LogisticaClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetRutasDia(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/rutas/dia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Logística: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetEnviosPendientes(string codigoSucursal)
        {
            try
            {
                var url = $"/api/envios/pendientes?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Logística: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetEstadoEntrega(string numeroGuia)
        {
            try
            {
                var url = $"/api/entregas/estado?guia={numeroGuia}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Logística: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetReporteLogistico(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/reportes/logistico?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Logística: {ex.Message}");
                return null;
            }
        }
    }
}
