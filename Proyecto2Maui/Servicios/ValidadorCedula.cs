using Proyecto2Maui.Modelos;
using System.Text.Json;

namespace Proyecto2Maui.Servicios
{
    public class ValidadorCedula : IValidadorCedula
    {
        //url de l aapi del gobierno
        const string URL = $"https://api.digital.gob.do/v3/cedulas/";
        const string ACCION = "/validate";

        //Peticion http a servicio del gobierno dominicano para validad cedula
        public async Task<EstadoCedula> ValidarCedula(string cedula)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(URL + cedula + ACCION);
            var content = await response.Content.ReadAsStringAsync();
            var estadoCedula = JsonSerializer.Deserialize<EstadoCedula>(content);

            return estadoCedula;
        }
    }
}
