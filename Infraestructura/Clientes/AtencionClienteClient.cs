using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class AtencionClienteClient : IAtencionClienteClient
    {
        private readonly HttpClient _httpClient;

        public AtencionClienteClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetReclamosActivos(string codigoSucursal)
        {
            try
            {
                var url = $"/api/reclamos/activos?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Atención al Cliente: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetReclamosDia(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/reclamos/dia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Atención al Cliente: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetSatisfaccionClientes(string codigoSucursal)
        {
            try
            {
                var url = $"/api/satisfaccion?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Atención al Cliente: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetDevolucionesCliente(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/devoluciones?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Atención al Cliente: {ex.Message}");
                return null;
            }
        }
    }
}
