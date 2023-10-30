using Proyecto2Maui.Modelos;
using Proyecto2Maui.Modelos.Request;

namespace Proyecto2Maui.Servicios
{
    public interface ITurnoServices
    {
        Task<List<Turno>> GetTurnos();
        Task<string> GenerarTurno(GenerarTurnoRequest turno);
        Task<string> CancelarTurno(string idTurno);
    }
}
