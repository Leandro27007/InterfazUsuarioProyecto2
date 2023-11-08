using DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Proyecto2Maui.Modelos;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Windows.Media.Protection.PlayReady;

namespace Proyecto2Maui.Servicios
{
    public class PruebaLabService : IPruebaLabService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://proyecto2web.azurewebsites.net/api/PruebaLab/";
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public PruebaLabService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpClientFactory = httpClientFactory;
            this._authenticationStateProvider = authenticationStateProvider;

        }

        public async Task<PruebaLab> CrearPruebaLab(CreacionPruebaLabDTO creacionPruebaLabDTO)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;

            try
            {
                // Crea una instancia de HttpClient utilizando la fábrica
                var httpClient = _httpClientFactory.CreateClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                // Construye la URL completa para la solicitud POST
                string apiUrl = $"{_Url}CrearPruebaLab";

                // Serializa el objeto CreacionPruebaLabDTO a JSON
                var jsonContent = JsonConvert.SerializeObject(creacionPruebaLabDTO);

                // Configura el contenido de la solicitud
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Realiza la solicitud POST
                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta y deserialízalo en un objeto PruebaLab
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var pruebaLab = JsonConvert.DeserializeObject<PruebaLab>(responseContent);
                    return pruebaLab;
                }
                else
                {
                    // Si la respuesta no es exitosa, maneja el error según tus necesidades
                    // Puedes lanzar una excepción, registrar el error, o tomar otra acción.
                    throw new Exception($"Error al crear una prueba de laboratorio. Estado de respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                // Aquí puedes registrar el error o tomar medidas específicas
                throw ex;
            }
        }

        public async Task<bool> EditarPruebaLab(EdicionPruebaLabDTO edicionPruebaLabDTO)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;

            try
            {
                // Crea una instancia de HttpClient utilizando la fábrica
                var httpClient = _httpClientFactory.CreateClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                // Construye la URL completa para la solicitud PUT
                string apiUrl = $"{_Url}EditarPruebaLab";

                // Serializa el objeto EdicionPruebaLabDTO a JSON
                var jsonContent = JsonConvert.SerializeObject(edicionPruebaLabDTO);

                // Configura el contenido de la solicitud
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT
                var response = await httpClient.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // La solicitud fue exitosa
                    return true;
                }
                else
                {
                    // Si la respuesta no es exitosa, maneja el error según tus necesidades
                    // Puedes lanzar una excepción, registrar el error, o tomar otra acción.
                    throw new Exception($"Error al editar la prueba de laboratorio. Estado de respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                // Aquí puedes registrar el error o tomar medidas específicas
                throw ex;
            }
        }

        public async Task<bool> eliminarPruebaLab(string idPruebalab)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;

            try
            {
                // Crea una instancia de HttpClient utilizando la fábrica
                var httpClient = _httpClientFactory.CreateClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Construye la URL completa para la solicitud DELETE con el id en el path
                string apiUrl = $"{_Url}EliminarPruebaLab {idPruebalab}";

                // Realiza la solicitud DELETE
                var response = await httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // La solicitud fue exitosa
                    return true;
                }
                else
                {
                    // Si la respuesta no es exitosa, maneja el error según tus necesidades
                    // Puedes lanzar una excepción, registrar el error, o tomar otra acción.
                    throw new Exception($"Error al eliminar la prueba de laboratorio. Estado de respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                // Aquí puedes registrar el error o tomar medidas específicas
                throw ex;
            }
        }

        public async Task<IEnumerable<PruebaLab>> ObtenerPruebas()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;



            try
            {
                using var client = _httpClientFactory.CreateClient();


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var respuesta = await client.GetAsync(this._Url + "ListarPruebas");

                var contenido = await respuesta.Content.ReadAsStringAsync();

                var pruebasLab = System.Text.Json.JsonSerializer.Deserialize<List<PruebaLab>>(contenido);

                return pruebasLab;

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
