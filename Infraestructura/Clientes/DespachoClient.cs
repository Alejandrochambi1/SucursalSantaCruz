using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class DespachoClient : IDespachoClient
    {
        private readonly HttpClient _httpClient;

        public DespachoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetDespachoDia(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/despachos/dia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Despacho: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetPedidosPendientes(string codigoSucursal)
        {
            try
            {
                var url = $"/api/pedidos/pendientes?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Despacho: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetReportePedidos(string codigoSucursal)
        {
            try
            {
                var url = $"/api/reportes/pedidos?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Despacho: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetVolumenDespacho(string codigoSucursal, string periodo)
        {
            try
            {
                var url = $"/api/volumen?sucursal={codigoSucursal}&periodo={periodo}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Despacho: {ex.Message}");
                return null;
            }
        }
    }
}
