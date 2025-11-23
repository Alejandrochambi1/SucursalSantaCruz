using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de ATENCIÓN AL CLIENTE
    /// GET: Traer reclamos, satisfacción, devoluciones de cliente
    /// </summary>
    public interface IAtencionClienteClient
    {
        Task<dynamic> GetReclamosActivos(string codigoSucursal);
        Task<dynamic> GetReclamosDia(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetSatisfaccionClientes(string codigoSucursal);
        Task<dynamic> GetDevolucionesCliente(string codigoSucursal, DateTime fecha);
    }
}
