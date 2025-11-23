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
    /// Implementación de ISolicitudRepository
    /// Accede a la base de datos para operaciones de Solicitudes
    /// </summary>
    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly SucursalDbContext _context;
        
        public SolicitudRepository(SucursalDbContext context)
        {
            _context = context;
        }
        
        public async Task<SolicitudInterna> GetById(Guid id)
        {
            return await _context.SolicitudesInternas
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        
        public async Task<List<SolicitudInterna>> GetPendientes(string codigoSucursal)
        {
            return await _context.SolicitudesInternas
                .Include(s => s.Items)
                .Where(s => s.CodigoSucursal == codigoSucursal && 
                           s.Estado == SolicitudInterna.EstadoSolicitud.Pendiente)
                .OrderByDescending(s => s.FechaCreacion)
                .ToListAsync();
        }
        
        public async Task<List<SolicitudInterna>> GetHistorico(string codigoSucursal, int limit = 10)
        {
            return await _context.SolicitudesInternas
                .Include(s => s.Items)
                .Where(s => s.CodigoSucursal == codigoSucursal)
                .OrderByDescending(s => s.FechaCreacion)
                .Take(limit)
                .ToListAsync();
        }
        
        public async Task<Guid> Add(SolicitudInterna solicitud)
        {
            _context.SolicitudesInternas.Add(solicitud);
            await _context.SaveChangesAsync();
            return solicitud.Id;
        }
        
        public async Task<bool> Update(SolicitudInterna solicitud)
        {
            _context.SolicitudesInternas.Update(solicitud);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
