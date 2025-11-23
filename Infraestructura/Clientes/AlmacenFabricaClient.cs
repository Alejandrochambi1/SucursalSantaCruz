using Sucursal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Clientes
{
    /// <summary>
    /// CLIENTE HTTP PARA ALMACÉN DE FÁBRICA
    /// Consumir: Catálogo de productos disponibles para reposición
    /// Producir: Crear pedidos de reposición
    /// Este es el servicio crítico para la lógica de reposición de Sucursal
    /// </summary>
    public class AlmacenFabricaClient : IAlmacenFabricaClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AlmacenFabricaClient> _logger;

        public AlmacenFabricaClient(HttpClient httpClient, ILogger<AlmacenFabricaClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// GET /api/almacen-fabrica/catalogo
        /// Traer catálogo completo de productos disponibles en fábrica
        /// Se llama cuando Sucursal necesita ver qué productos puede pedir
        /// </summary>
        public async Task<dynamic> ObtenerCatalogo()
        {
            try
            {
                _logger.LogInformation($"[ALMACEN-FABRICA] Pidiendo catálogo de productos");
                var response = await _httpClient.GetAsync("/api/almacen-fabrica/catalogo");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[ALMACEN-FABRICA] Catálogo obtenido: {content}");
                    return content;
                }
                else
                {
                    _logger.LogError($"[ALMACEN-FABRICA] Error {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ALMACEN-FABRICA] Excepción: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// POST /api/almacen-fabrica/pedidos
        /// Crear un pedido de reposición en el almacén de fábrica
        /// Retorna el ID del pedido creado
        /// </summary>
        public async Task<Guid> CrearPedido(dynamic pedido)
        {
            try
            {
                _logger.LogInformation($"[ALMACEN-FABRICA] Creando pedido de reposición");

                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(pedido),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync("/api/almacen-fabrica/pedidos", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[ALMACEN-FABRICA] Pedido creado: {responseContent}");

                    // Supone que retorna un objeto con { id: "uuid" }
                    var result = System.Text.Json.JsonDocument.Parse(responseContent);
                    if (result.RootElement.TryGetProperty("id", out var idElement))
                    {
                        return Guid.Parse(idElement.GetString());
                    }
                    return Guid.Empty;
                }
                else
                {
                    _logger.LogError($"[ALMACEN-FABRICA] Error {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
                    return Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ALMACEN-FABRICA] Excepción: {ex.Message}");
                return Guid.Empty;
            }
        }

        /// <summary>
        /// GET /api/almacen-fabrica/pedidos/{idPedido}
        /// Obtener estado de un pedido (Pendiente, Enviado, Entregado)
        /// </summary>
        public async Task<dynamic> ObtenerEstadoPedido(Guid idPedido)
        {
            try
            {
                _logger.LogInformation($"[ALMACEN-FABRICA] Pidiendo estado del pedido {idPedido}");
                var response = await _httpClient.GetAsync($"/api/almacen-fabrica/pedidos/{idPedido}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"[ALMACEN-FABRICA] Estado del pedido: {content}");
                    return content;
                }
                else
                {
                    _logger.LogError($"[ALMACEN-FABRICA] Error {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ALMACEN-FABRICA] Excepción: {ex.Message}");
                return null;
            }
        }
    }
}
