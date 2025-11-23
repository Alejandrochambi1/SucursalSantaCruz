using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de RECEPCIÓN
    /// GET: Traer materias primas recibidas, proveedores, calidad de entrada
    /// </summary>
    public interface IRecepcionClient
    {
        Task<dynamic> GetRecepcionesDia(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetMateriasRecibidas(string codigoSucursal);
        Task<dynamic> GetReporteProveedores(string codigoSucursal);
        Task<dynamic> GetControlesCalidad(string codigoSucursal, DateTime fecha);
    }
}
