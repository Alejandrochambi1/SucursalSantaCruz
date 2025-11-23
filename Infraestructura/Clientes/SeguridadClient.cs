using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class SeguridadClient : ISeguridadClient
    {
        private readonly HttpClient _httpClient;

        public SeguridadClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetIncidentesDia(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/incidentes/dia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Seguridad: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetAlertasSeguridad(string codigoSucursal)
        {
            try
            {
                var url = $"/api/alertas?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Seguridad: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetRegistroAccesos(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/accesos?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Seguridad: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetReporteSeguridad(string codigoSucursal, string periodo)
        {
            try
            {
                var url = $"/api/reportes/seguridad?sucursal={codigoSucursal}&periodo={periodo}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Seguridad: {ex.Message}");
                return null;
            }
        }
    }
}
