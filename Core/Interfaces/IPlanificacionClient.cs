using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de PLANIFICACIÓN
    /// GET: Traer demanda, proyecciones, plan de producción
    /// </summary>
    public interface IPlanificacionClient
    {
        Task<dynamic> GetProyeccionesDemanda(string codigoSucursal, string periodo);
        Task<dynamic> GetPlanProduccion(string codigoSucursal, string mes);
        Task<dynamic> GetForecast(string codigoSucursal);
        Task<dynamic> GetReportePlanificacion(string codigoSucursal, string trimestre);
    }
}
