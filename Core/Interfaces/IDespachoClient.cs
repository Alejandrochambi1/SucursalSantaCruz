using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de DESPACHO
    /// GET: Traer salidas de productos, pedidos despachados, estado
    /// </summary>
    public interface IDespachoClient
    {
        Task<dynamic> GetDespachoDia(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetPedidosPendientes(string codigoSucursal);
        Task<dynamic> GetReportePedidos(string codigoSucursal);
        Task<dynamic> GetVolumenDespacho(string codigoSucursal, string periodo);
    }
}
