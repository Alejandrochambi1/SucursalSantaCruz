using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de LOGÍSTICA
    /// GET: Traer rutas, envíos, estado de entregas
    /// </summary>
    public interface ILogisticaClient
    {
        Task<dynamic> GetRutasDia(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetEnviosPendientes(string codigoSucursal);
        Task<dynamic> GetEstadoEntrega(string numeroGuia);
        Task<dynamic> GetReporteLogistico(string codigoSucursal, DateTime fecha);
    }
}
