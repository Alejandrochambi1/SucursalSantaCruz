using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class RecepcionClient : IRecepcionClient
    {
        private readonly HttpClient _httpClient;

        public RecepcionClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetRecepcionesDia(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/recepciones/dia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Recepción: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetMateriasRecibidas(string codigoSucursal)
        {
            try
            {
                var url = $"/api/materias?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Recepción: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetReporteProveedores(string codigoSucursal)
        {
            try
            {
                var url = $"/api/proveedores/reporte?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Recepción: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetControlesCalidad(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/controles/entrada?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Recepción: {ex.Message}");
                return null;
            }
        }
    }
}
