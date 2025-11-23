using Sucursal.Core.Entidades;
using Sucursal.Core.DTOs;
using System;

namespace Sucursal.Core.Mappers
{
    public static class SolicitudMapper
    {
        /// <summary>
        /// Transforma CrearSolicitudCommand en SolicitudInterna
        /// </summary>
        public static SolicitudInterna ToEntity(this CrearSolicitudCommand command)
        {
            var solicitud = new SolicitudInterna
            {
                CodigoSucursal = command.CodigoSucursal,
                Tipo = Enum.Parse<SolicitudInterna.TipoSolicitud>(command.Tipo),
                Descripcion = command.Descripcion,
                MontoSolicitado = command.MontoSolicitado,
                CISolicitante = command.CISolicitante,
                Estado = SolicitudInterna.EstadoSolicitud.Pendiente,
                FechaCreacion = DateTime.UtcNow
            };
            
            // Agregar items
            foreach (var itemDto in command.Items)
            {
                solicitud.Items.Add(new ItemSolicitud
                {
                    CodigoArticulo = itemDto.CodigoArticulo,
                    NombreArticulo = itemDto.NombreArticulo,
                    Cantidad = itemDto.Cantidad,
                    PrecioUnitario = itemDto.PrecioUnitario
                });
            }
            
            return solicitud;
        }
        
        /// <summary>
        /// Transforma SolicitudInterna en SolicitudResumenDTO
        /// </summary>
        public static SolicitudResumenDTO ToResumenDTO(this SolicitudInterna solicitud)
        {
            return new SolicitudResumenDTO
            {
                Id = solicitud.Id,
                Tipo = solicitud.Tipo.ToString(),
                Estado = solicitud.Estado.ToString(),
                FechaCreacion = solicitud.FechaCreacion,
                Monto = solicitud.MontoSolicitado
            };
        }
    }
}
