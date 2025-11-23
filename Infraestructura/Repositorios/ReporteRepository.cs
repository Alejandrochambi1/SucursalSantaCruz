using Microsoft.EntityFrameworkCore;
using Sucursal.Core.Entidades;
using Sucursal.Core.Interfaces;
using Sucursal.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Repositorios
{
    /// <summary>
    /// Implementación de IReporteRepository
    /// Accede a la base de datos para operaciones de Reportes
    /// </summary>
    public class ReporteRepository : IReporteRepository
    {
        private readonly SucursalDbContext _context;
        
        public ReporteRepository(SucursalDbContext context)
        {
            _context = context;
        }
        
        public async Task<ReporteDiarioConsolidado> GetDiario(string codigoSucursal, DateTime fecha)
        {
            return await _context.ReportesDiarios
                .FirstOrDefaultAsync(r => r.CodigoSucursal == codigoSucursal && 
                                          r.Fecha.Date == fecha.Date);
        }
        
        public async Task<List<ReporteDiarioConsolidado>> GetMensual(string codigoSucursal, int mes, int anio)
        {
            return await _context.ReportesDiarios
                .Where(r => r.CodigoSucursal == codigoSucursal && 
                           r.Fecha.Month == mes && 
                           r.Fecha.Year == anio)
                .OrderBy(r => r.Fecha)
                .ToListAsync();
        }
        
        public async Task<Guid> Add(ReporteDiarioConsolidado reporte)
        {
            _context.ReportesDiarios.Add(reporte);
            await _context.SaveChangesAsync();
            return reporte.Id;
        }
        
        public async Task<bool> Update(ReporteDiarioConsolidado reporte)
        {
            _context.ReportesDiarios.Update(reporte);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
