using Proyecto2Maui.Modelos;

namespace Proyecto2Maui.Servicios
{
    public interface ITurnoServices
    {
        Task<List<Turno>> GetTunos();

    }
}
