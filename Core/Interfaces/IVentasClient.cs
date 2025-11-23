using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de VENTAS
    /// GET: Traer reporte de ventas del día
    /// </summary>
    public interface IVentasClient
    {
        Task<dynamic> GetReporteDiario(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetReporteMensual(string codigoSucursal, int mes, int anio);
        Task<dynamic> GetTotalVentas(string codigoSucursal);
    }
}
