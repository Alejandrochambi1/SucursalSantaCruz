using Sucursal.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para acceder a datos de Reportes en base de datos
    /// </summary>
    public interface IReporteRepository
    {
        Task<ReporteDiarioConsolidado> GetDiario(string codigoSucursal, DateTime fecha);
        Task<List<ReporteDiarioConsolidado>> GetMensual(string codigoSucursal, int mes, int anio);
        Task<Guid> Add(ReporteDiarioConsolidado reporte);
        Task<bool> Update(ReporteDiarioConsolidado reporte);
    }
}
