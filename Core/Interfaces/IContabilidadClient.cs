using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de CONTABILIDAD
    /// GET: Traer presupuesto disponible, gastos, estados financieros
    /// </summary>
    public interface IContabilidadClient
    {
        Task<dynamic> GetPresupuesto(string codigoSucursal, string centro);
        Task<dynamic> GetGastos(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetEstadosFinancieros(string codigoSucursal, string periodo);
        Task<dynamic> GetPresupuestoDisponible(string codigoSucursal);
    }
}
