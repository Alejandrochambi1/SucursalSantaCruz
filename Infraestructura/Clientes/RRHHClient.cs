using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sucursal.Infraestructura.Clientes
{
    /// <summary>
    /// Cliente HTTP para consumir el microservicio de RRHH
    /// GET: Traer empleados, asistencia, nómina, gastos laborales
    /// </summary>
    public class RRHHClient : IRRHHClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        
        public RRHHClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["MicroservicesUrls:RRHH"] ?? "http://localhost:5003";
        }
        
        public async Task<dynamic> GetEmpleados(string codigoSucursal)
        {
            try
            {
                var url = $"{_baseUrl}/api/empleados?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo RRHH: {ex.Message}");
                return null;
            }
        }
        
        public async Task<dynamic> GetAsistencia(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"{_baseUrl}/api/asistencia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo RRHH: {ex.Message}");
                return null;
            }
        }
        
        public async Task<dynamic> GetNomina(string codigoSucursal, string periodo)
        {
            try
            {
                var url = $"{_baseUrl}/api/nomina?sucursal={codigoSucursal}&periodo={periodo}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo RRHH: {ex.Message}");
                return null;
            }
        }
        
        public async Task<dynamic> GetGastoLaboralDia(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"{_baseUrl}/api/gastos/dia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo RRHH: {ex.Message}");
                return null;
            }
        }
    }
}
