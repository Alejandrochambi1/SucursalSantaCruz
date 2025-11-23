using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de TESORERÍA
    /// GET: Traer caja, efectivo, flujo de efectivo
    /// </summary>
    public interface ITesoreriaClient
    {
        Task<dynamic> GetSaldoCaja(string codigoSucursal);
        Task<dynamic> GetFlujoCaja(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetMovimientosEfectivo(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetReporteTesoria(string codigoSucursal, string periodo);
    }
}
