using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class MantenimientoClient : IMantenimientoClient
    {
        private readonly HttpClient _httpClient;

        public MantenimientoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetEquiposDisponibles()
        {
            try
            {
                var url = "/api/equipos/disponibles";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Mantenimiento: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetParadasMaquinas(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/paradas?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Mantenimiento: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetOrdenesMantenimiento(string codigoSucursal)
        {
            try
            {
                var url = $"/api/ordenes?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Mantenimiento: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetTiempoInactividad(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/inactividad?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Mantenimiento: {ex.Message}");
                return null;
            }
        }
    }
}
