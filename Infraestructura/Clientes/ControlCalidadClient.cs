using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class ControlCalidadClient : IControlCalidadClient
    {
        private readonly HttpClient _httpClient;

        public ControlCalidadClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetInspeccionesDia(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/inspecciones/dia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Control de Calidad: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetProductosRechazados(string codigoSucursal)
        {
            try
            {
                var url = $"/api/productos/rechazados?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Control de Calidad: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetRetirosProducto(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/retiros?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Control de Calidad: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetTasaRechazo(string codigoSucursal, string periodo)
        {
            try
            {
                var url = $"/api/tasa-rechazo?sucursal={codigoSucursal}&periodo={periodo}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Control de Calidad: {ex.Message}");
                return null;
            }
        }
    }
}
