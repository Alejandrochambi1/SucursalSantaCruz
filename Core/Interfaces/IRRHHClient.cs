using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de RECURSOS HUMANOS
    /// GET: Traer datos de empleados, presencia, nomina
    /// </summary>
    public interface IRRHHClient
    {
        Task<dynamic> GetEmpleados(string codigoSucursal);
        Task<dynamic> GetAsistencia(string codigoSucursal, DateTime fecha);
        Task<dynamic> GetNomina(string codigoSucursal, string periodo);
        Task<dynamic> GetGastoLaboralDia(string codigoSucursal, DateTime fecha);
    }
}
