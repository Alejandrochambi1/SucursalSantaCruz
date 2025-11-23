using Sucursal.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    public class FacturacionClient : IFacturacionClient
    {
        private readonly HttpClient _httpClient;

        public FacturacionClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetFacturasDia(string codigoSucursal, DateTime fecha)
        {
            try
            {
                var url = $"/api/facturas/dia?sucursal={codigoSucursal}&fecha={fecha:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Facturación: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetDocumentosPendientes(string codigoSucursal)
        {
            try
            {
                var url = $"/api/documentos/pendientes?sucursal={codigoSucursal}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Facturación: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetImpuestosPeriodo(string codigoSucursal, string periodo)
        {
            try
            {
                var url = $"/api/impuestos?sucursal={codigoSucursal}&periodo={periodo}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Facturación: {ex.Message}");
                return null;
            }
        }

        public async Task<dynamic> GetReporteFacturacion(string codigoSucursal, string mes)
        {
            try
            {
                var url = $"/api/reportes/facturacion?sucursal={codigoSucursal}&mes={mes}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Facturación: {ex.Message}");
                return null;
            }
        }
    }
}
