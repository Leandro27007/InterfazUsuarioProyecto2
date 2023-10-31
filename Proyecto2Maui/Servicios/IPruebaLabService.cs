using Proyecto2Maui.Modelos;

namespace Proyecto2Maui.Servicios
{
    public interface IPruebaLabService
    {
        Task<IEnumerable<PruebaLab>> ObtenerPruebas();

    }
}
