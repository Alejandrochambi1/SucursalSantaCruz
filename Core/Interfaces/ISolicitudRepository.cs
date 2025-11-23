using Sucursal.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para acceder a datos de Solicitudes en base de datos
    /// </summary>
    public interface ISolicitudRepository
    {
        Task<SolicitudInterna> GetById(Guid id);
        Task<List<SolicitudInterna>> GetPendientes(string codigoSucursal);
        Task<List<SolicitudInterna>> GetHistorico(string codigoSucursal, int limit = 10);
        Task<Guid> Add(SolicitudInterna solicitud);
        Task<bool> Update(SolicitudInterna solicitud);
    }
}
