using System;
using System.Threading.Tasks;

namespace Sucursal.Core.Interfaces
{
    /// <summary>
    /// Contrato para consumir el microservicio de MARKETING
    /// GET: Traer campañas, presupuesto disponible, resultados
    /// </summary>
    public interface IMarketingClient
    {
        Task<dynamic> GetCampanasActivas(string codigoSucursal);
        Task<dynamic> GetPresupuestoCampanas(string codigoSucursal);
        Task<dynamic> GetResultadosCampana(Guid idCampana);
    }
}
