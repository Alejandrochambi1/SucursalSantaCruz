using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de PRODUCCIÓN
    /// GET: Traer órdenes de producción, capacidad, estado de líneas
    /// </summary>
    public interface IProduccionClient
    {
        Task<dynamic> GetOrdenesProduccion(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetCapacidadPlanta();
        Task<dynamic> GetEstadoLineasProduccion();
        Task<dynamic> GetReporteProduccionDia(DateTime fecha);
        Task<dynamic> GetParosProduccion(string codigoSucursal, DateTime fecha);
    }
}
