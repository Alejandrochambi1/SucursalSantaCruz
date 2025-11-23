using Sucursal.Core.DTOs;
using Sucursal.Core.Entidades;
using Sucursal.Core.Interfaces;
using Sucursal.Core.Mappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sucursal.Infraestructura.Servicios
{
    /// <summary>
    /// Servicio de aplicación para gestionar Solicitudes de Reposición y Marketing
    /// CREA solicitudes, las APRUEBA/RECHAZA, y las EJECUTA en otros servicios
    /// </summary>
    public class SolicitudesService
    {
        private readonly ISolicitudRepository _solicitudRepository;
        private readonly IAlmacenFabricaClient _almacenFabricaClient;
        private readonly IContabilidadClient _contabilidadClient;
        
        public SolicitudesService(
            ISolicitudRepository solicitudRepository,
            IAlmacenFabricaClient almacenFabricaClient,
            IContabilidadClient contabilidadClient)
        {
            _solicitudRepository = solicitudRepository;
            _almacenFabricaClient = almacenFabricaClient;
            _contabilidadClient = contabilidadClient;
        }
        
        /// <summary>
        /// CREAR SOLICITUD DE REPOSICIÓN
        /// Ej: "Necesito 1000 bolsas de leche deslactosada"
        /// </summary>
        public async Task<Guid> CrearSolicitudReposicion(CrearSolicitudCommand command)
        {
            // 1. Validar que sea tipo reposición
            if (command.Tipo != "Reposicion")
                throw new ArgumentException("Tipo de solicitud inválido");
            
            // 2. Transformar comando a entidad
            var solicitud = command.ToEntity();
            solicitud.Tipo = SolicitudInterna.TipoSolicitud.Reposicion;
            
            // 3. Calcular monto total de la solicitud
            decimal montoTotal = 0;
            foreach (var item in solicitud.Items)
            {
                montoTotal += item.Subtotal;
            }
            solicitud.MontoSolicitado = montoTotal;
            
            // 4. Guardar en base de datos
            var idSolicitud = await _solicitudRepository.Add(solicitud);
            
            return idSolicitud;
        }
        
        /// <summary>
        /// CREAR SOLICITUD DE MARKETING
        /// Ej: "Campaña San Valentín Cruceño por $500"
        /// </summary>
        public async Task<Guid> CrearSolicitudMarketing(CrearSolicitudCommand command)
        {
            // 1. Validar que sea tipo marketing
            if (command.Tipo != "Marketing")
                throw new ArgumentException("Tipo de solicitud inválido");
            
            // 2. Validar que tenga monto
            if (!command.MontoSolicitado.HasValue || command.MontoSolicitado <= 0)
                throw new ArgumentException("Marketing requiere monto > 0");
            
            // 3. Transformar comando a entidad
            var solicitud = command.ToEntity();
            solicitud.Tipo = SolicitudInterna.TipoSolicitud.Marketing;
            
            // 4. Guardar en base de datos
            var idSolicitud = await _solicitudRepository.Add(solicitud);
            
            return idSolicitud;
        }
        
        /// <summary>
        /// APROBAR SOLICITUD (por Alejandro, el gerente)
        /// - Para Reposición: Valida presupuesto y envía a Almacén de Fábrica
        /// - Para Marketing: Valida presupuesto y registra en Marketing
        /// </summary>
        public async Task<bool> AprobarSolicitud(Guid idSolicitud, string ciAprobador)
        {
            // 1. Obtener solicitud
            var solicitud = await _solicitudRepository.GetById(idSolicitud);
            if (solicitud == null)
                throw new ArgumentException("Solicitud no encontrada");
            
            // 2. Validar estado
            if (solicitud.Estado != SolicitudInterna.EstadoSolicitud.Pendiente)
                throw new ArgumentException("Solo se pueden aprobar solicitudes pendientes");
            
            // 3. Validar presupuesto con Contabilidad
            var presupuesto = await _contabilidadClient.GetPresupuesto(
                solicitud.CodigoSucursal, 
                solicitud.Tipo.ToString());
            
            if (presupuesto == null || presupuesto["disponible"] < solicitud.MontoSolicitado)
                throw new ArgumentException("Presupuesto insuficiente");
            
            // 4. Actualizar estado
            solicitud.Estado = SolicitudInterna.EstadoSolicitud.Aprobada;
            solicitud.CIAprobador = ciAprobador;
            solicitud.FechaAprobacion = DateTime.UtcNow;
            
            // 5. Guardar cambios
            await _solicitudRepository.Update(solicitud);
            
            // 6. Ejecutar según tipo
            if (solicitud.Tipo == SolicitudInterna.TipoSolicitud.Reposicion)
            {
                await EjecutarReposicion(solicitud);
            }
            else if (solicitud.Tipo == SolicitudInterna.TipoSolicitud.Marketing)
            {
                await EjecutarMarketing(solicitud);
            }
            
            return true;
        }
        
        /// <summary>
        /// RECHAZAR SOLICITUD (por Alejandro)
        /// </summary>
        public async Task<bool> RechazarSolicitud(Guid idSolicitud, string ciAprobador, string motivo)
        {
            // 1. Obtener solicitud
            var solicitud = await _solicitudRepository.GetById(idSolicitud);
            if (solicitud == null)
                throw new ArgumentException("Solicitud no encontrada");
            
            // 2. Validar estado
            if (solicitud.Estado != SolicitudInterna.EstadoSolicitud.Pendiente)
                throw new ArgumentException("Solo se pueden rechazar solicitudes pendientes");
            
            // 3. Actualizar estado
            solicitud.Estado = SolicitudInterna.EstadoSolicitud.Rechazada;
            solicitud.CIAprobador = ciAprobador;
            solicitud.FechaAprobacion = DateTime.UtcNow;
            solicitud.MotivoRechazo = motivo;
            
            // 4. Guardar cambios
            await _solicitudRepository.Update(solicitud);
            
            return true;
        }
        
        /// <summary>
        /// EJECUTAR REPOSICIÓN: Envía el pedido a Almacén de Fábrica
        /// </summary>
        private async Task EjecutarReposicion(SolicitudInterna solicitud)
        {
            try
            {
                // Crear estructura para Almacén de Fábrica
                var pedido = new
                {
                    codigoSucursal = solicitud.CodigoSucursal,
                    items = solicitud.Items,
                    montoTotal = solicitud.MontoSolicitado,
                    fechaPedido = DateTime.UtcNow
                };
                
                // Enviar POST a Almacén de Fábrica
                var idPedido = await _almacenFabricaClient.CrearPedido(pedido);
                
                // Actualizar solicitud con ID del pedido en fábrica
                solicitud.DetallesJSON = System.Text.Json.JsonSerializer.Serialize(new { idPedidoFabrica = idPedido });
                solicitud.Estado = SolicitudInterna.EstadoSolicitud.EnProgreso;
                await _solicitudRepository.Update(solicitud);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ejecutando reposición: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// EJECUTAR MARKETING: Registra la campaña aprobada
        /// </summary>
        private async Task EjecutarMarketing(SolicitudInterna solicitud)
        {
            try
            {
                // Aquí se enviaría a Marketing para que cree la campaña
                // Por ahora solo marcamos como completada
                
                solicitud.Estado = SolicitudInterna.EstadoSolicitud.Completada;
                solicitud.FechaCompletacion = DateTime.UtcNow;
                await _solicitudRepository.Update(solicitud);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ejecutando marketing: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// OBTENER SOLICITUDES PENDIENTES
        /// </summary>
        public async Task<List<SolicitudInterna>> ObtenerPendientes(string codigoSucursal)
        {
            return await _solicitudRepository.GetPendientes(codigoSucursal);
        }
    }
}
