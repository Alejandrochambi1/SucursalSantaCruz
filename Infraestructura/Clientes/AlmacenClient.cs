using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sucursal.Infraestructura.Clientes
{
    /// <summary>
    /// Cliente HTTP para consumir el microservicio de ALMACEN
    /// Llamadas GET para obtener datos de inventario, productos vencidos, etc.
    /// </summary>
    public class AlmacenClient : IAlmacenClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        
        public AlmacenClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["MicroservicesUrls:Almacen"] ?? "http://localhost:5002";
        }
        
        public async Task<dynamic> GetInventario(string codigoSucursal)
        {
            try
            {
                var url = $"{_baseUrl}/api/inventario?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Almacén: {ex.Message}");
                return null;
            }
        }
        
        public async Task<dynamic> GetProductosVencidos(string codigoSucursal)
        {
            try
            {
                var url = $"{_baseUrl}/api/inventario/vencidos?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Almacén: {ex.Message}");
                return null;
            }
        }
        
        public async Task<dynamic> GetProductosDevueltos(string codigoSucursal)
        {
            try
            {
                var url = $"{_baseUrl}/api/inventario/devueltos?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Almacén: {ex.Message}");
                return null;
            }
        }
        
        public async Task<dynamic> GetReporteInventarioDiario(string codigoSucursal, DateTime fecha)
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
                Console.WriteLine($"Error consumiendo Almacén: {ex.Message}");
                return null;
            }
        }
        
        public async Task<dynamic> GetStockValorado(string codigoSucursal)
        {
            try
            {
                var url = $"{_baseUrl}/api/inventario/valorado?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Almacén: {ex.Message}");
                return null;
            }
        }
    }
}
