using System;

namespace Sucursal.Core.DTOs
{
    /// <summary>
    /// Comando para APROBAR o RECHAZAR una solicitud
    /// </summary>
    public class ActualizarEstadoSolicitudCommand
    {
        public Guid IdSolicitud { get; set; }
        public string NuevoEstado { get; set; } // "Aprobada" o "Rechazada"
        public string CIAprobador { get; set; } // CI de Alejandro
        public string MotivoRechazo { get; set; } // Solo si es rechazada
    }
}
