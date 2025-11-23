using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de CONTROL DE CALIDAD
    /// GET: Traer inspecciones, retiros de producto, rechazos
    /// </summary>
    public interface IControlCalidadClient
    {
        Task<dynamic> GetInspeccionesDia(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetProductosRechazados(string codigoSucursal);
        Task<dynamic> GetRetirosProducto(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetTasaRechazo(string codigoSucursal, string periodo);
    }
}
