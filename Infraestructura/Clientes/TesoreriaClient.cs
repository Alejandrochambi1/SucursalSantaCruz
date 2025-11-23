using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class TesoreriaClient : ITesoreriaClient
    {
        private readonly HttpClient _httpClient;

        public TesoreriaClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetSaldoCaja(string codigoSucursal)
        {
            try
            {
                var url = $"/api/caja/saldo?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Tesorería: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetFlujoCaja(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/flujo-caja?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Tesorería: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetMovimientosEfectivo(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/movimientos?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Tesorería: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetReporteTesoria(string codigoSucursal, string periodo)
        {
            try
            {
                var url = $"/api/reportes/tesoreria?sucursal={codigoSucursal}&periodo={periodo}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Tesorería: {ex.Message}");
                return null;
            }
        }
    }
}
