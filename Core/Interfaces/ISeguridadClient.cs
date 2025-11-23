using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de SEGURIDAD
    /// GET: Traer incidentes, alertas de seguridad, accesos
    /// </summary>
    public interface ISeguridadClient
    {
        Task<dynamic> GetIncidentesDia(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetAlertasSeguridad(string codigoSucursal);
        Task<dynamic> GetRegistroAccesos(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetReporteSeguridad(string codigoSucursal, string periodo);
    }
}
