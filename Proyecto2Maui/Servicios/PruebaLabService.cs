using Proyecto2Maui.Modelos;
using System.Text.Json;

namespace Proyecto2Maui.Servicios
{
    public class PruebaLabService : IPruebaLabService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://proyecto2web.azurewebsites.net/api/PruebaLab/ListarPruebas";
        public PruebaLabService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<PruebaLab>> ObtenerPruebas()
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();

                var respuesta = await client.GetAsync(this._Url);

                var contenido = await respuesta.Content.ReadAsStringAsync();

                var pruebasLab = JsonSerializer.Deserialize<List<PruebaLab>>(contenido);

                return pruebasLab;

            }
            catch (Exception ex)
            {

                throw;
            }



        }
    }
}
