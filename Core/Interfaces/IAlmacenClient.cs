using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de ALMACEN
    /// GET: Traer datos de stock, productos vencidos, devoluciones
    /// </summary>
    public interface IAlmacenClient
    {
        Task<dynamic> GetInventario(string codigoSucursal);
        Task<dynamic> GetProductosVencidos(string codigoSucursal);
        Task<dynamic> GetProductosDevueltos(string codigoSucursal);
        Task<dynamic> GetReporteInventarioDiario(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetStockValorado(string codigoSucursal);
    }
}
