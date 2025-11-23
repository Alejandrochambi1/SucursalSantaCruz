using System.Net;
using System.Text;
using System.Text.Json;
using System.Net.Http; // <--- IMPORTANTE

namespace Sucursal.Infraestructura.Mocks
{
    // CAMBIO AQUÍ: Debe heredar de DelegatingHandler
    public class JsonMockHandler : DelegatingHandler
    {
        private readonly JsonElement _jsonData;

        public JsonMockHandler()
        {
            if (File.Exists("mock-data.json"))
            {
                var text = File.ReadAllText("mock-data.json");
                _jsonData = JsonSerializer.Deserialize<JsonElement>(text);
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Si no hay JSON, dejamos pasar la petición (o lanzamos error)
            if (_jsonData.ValueKind == JsonValueKind.Undefined)
            {
                // Opción A: Error
                // return new HttpResponseMessage(HttpStatusCode.InternalServerError) 
                // { Content = new StringContent("Error: No se encontró mock-data.json") };

                // Opción B: Si no hay mock, intentar llamar a internet (base.SendAsync)
                return await base.SendAsync(request, cancellationToken);
            }

            var seccionConsumo = _jsonData.GetProperty("CONSUMO_DESDE_OTROS_MICROSERVICIOS");
            object respuestaObj = null;

            int puerto = request.RequestUri.Port;

            try
            {
                switch (puerto)
                {
                    case 5001: // Ventas
                        respuestaObj = seccionConsumo.GetProperty("ejemplo_ventas_get").GetProperty("respuesta_esperada");
                        break;
                    case 5002: // Almacen
                        respuestaObj = seccionConsumo.GetProperty("ejemplo_almacen_get").GetProperty("respuesta_esperada");
                        break;
                    case 5004: // Contabilidad
                        respuestaObj = seccionConsumo.GetProperty("ejemplo_contabilidad_get").GetProperty("respuesta_esperada");
                        break;
                    case 5005: // Marketing
                        respuestaObj = seccionConsumo.GetProperty("ejemplo_marketing_get").GetProperty("respuesta_esperada");
                        break;

                    // Si el puerto no está en el switch, dejamos que intente conectar a internet real
                    default:
                        return await base.SendAsync(request, cancellationToken);
                }
            }
            catch
            {
                // Si falla al buscar en el JSON, también intentamos conectar real
                return await base.SendAsync(request, cancellationToken);
            }

            // Si encontramos el mock, devolvemos la respuesta falsa
            var jsonResponse = JsonSerializer.Serialize(respuestaObj);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json"),
                RequestMessage = request
            };

            return await Task.FromResult(response);
        }
    }
}