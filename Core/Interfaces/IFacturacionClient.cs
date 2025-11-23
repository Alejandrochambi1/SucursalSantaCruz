using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de FACTURACIÓN
    /// GET: Traer facturas, documentos, impuestos
    /// </summary>
    public interface IFacturacionClient
    {
        Task<dynamic> GetFacturasDia(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetDocumentosPendientes(string codigoSucursal);
        Task<dynamic> GetImpuestosPeriodo(string codigoSucursal, string periodo);
        Task<dynamic> GetReporteFacturacion(string codigoSucursal, string mes);
    }
}
