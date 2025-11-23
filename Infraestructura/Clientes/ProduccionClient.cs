using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class ProduccionClient : IProduccionClient
    {
        private readonly HttpClient _httpClient;

        public ProduccionClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetOrdenesProduccion(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/ordenes/dia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Producción: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetCapacidadPlanta()
        {
            try
            {
                var url = "/api/planta/capacidad";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Producción: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetEstadoLineasProduccion()
        {
            try
            {
                var url = "/api/lineas/estado";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Producción: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetReporteProduccionDia(DateTime fecha)
        {
            try
            {
                var url = $"/api/reportes/produccion?fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Producción: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetParosProduccion(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/paros?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Producción: {ex.Message}");
                return null;
            }
        }
    }
}
