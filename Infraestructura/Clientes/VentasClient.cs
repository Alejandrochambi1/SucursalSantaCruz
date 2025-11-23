using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    /// <summary>
    /// Cliente HTTP para consumir el microservicio de VENTAS
    /// Llamadas GET para obtener reportes de ventas
    /// </summary>
    public class VentasClient : IVentasClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        
        public VentasClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["MicroservicesUrls:Ventas"] ?? "http://localhost:5001";
        }
        
        public async Task<dynamic> GetReporteDiario(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"{_baseUrl}/api/reportes/diario?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Ventas: {ex.Message}");
                return null;
            }
        }
        
        public async Task<dynamic> GetReporteMensual(string codigoSucursal, int mes, int anio)
        {
            try
            {
                var url = $"{_baseUrl}/api/reportes/mensual?sucursal={codigoSucursal}&mes={mes}&anio={anio}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Ventas: {ex.Message}");
                return null;
            }
        }
        
        public async Task<dynamic> GetTotalVentas(string codigoSucursal)
        {
            try
            {
                var url = $"{_baseUrl}/api/reportes/totales?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Ventas: {ex.Message}");
                return null;
            }
        }
    }
}
