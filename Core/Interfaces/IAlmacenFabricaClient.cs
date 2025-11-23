using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de ALMACEN DE FABRICA
    /// GET: Traer catálogo de productos
    /// POST: Crear pedido de reposición
    /// </summary>
    public interface IAlmacenFabricaClient
    {
        Task<dynamic> ObtenerCatalogo();
        Task<Guid> CrearPedido(dynamic pedido);
        Task<dynamic> ObtenerEstadoPedido(Guid idPedido);
    }
}
