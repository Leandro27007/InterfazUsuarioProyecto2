using DTOs;

namespace Proyecto2Maui.Servicios
{
    public interface IReporteService
    {
        Task<ReporteDTO> ObtenerReporte(string FechaInicio, string FechaFinal); 
    }
}
