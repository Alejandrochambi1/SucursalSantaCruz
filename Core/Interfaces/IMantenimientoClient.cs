using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de MANTENIMIENTO
    /// GET: Traer equipos, paradas, órdenes de mantenimiento
    /// </summary>
    public interface IMantenimientoClient
    {
        Task<dynamic> GetEquiposDisponibles();
        Task<dynamic> GetParadasMaquinas(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetOrdenesMantenimiento(string codigoSucursal);
        Task<dynamic> GetTiempoInactividad(string codigoSucursal, DateTime fecha);
    }
}
