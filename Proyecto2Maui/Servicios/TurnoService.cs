using Microsoft.AspNetCore.Components.Authorization;
using Proyecto2Maui.Modelos;
using Proyecto2Maui.Modelos.Request;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Proyecto2Maui.Servicios
{
    public class TurnoService : ITurnoServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://proyecto2web.azurewebsites.net/api/Turno/";
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public TurnoService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpClientFactory = httpClientFactory;
            this._authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<string> GenerarTurno(GenerarTurnoRequest turnoParaGenerar)
        {

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;

            try
            {
                using var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                var turnoJson = JsonSerializer.Serialize(turnoParaGenerar);

                var contenido = new StringContent(turnoJson, Encoding.UTF8, "application/json");

                var respuesta = await client.PostAsync(_Url + "GenerarTurno", contenido);

                respuesta.EnsureSuccessStatusCode();


                var respuestaARetornar = await respuesta.Content.ReadAsStringAsync();

                return respuestaARetornar;
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public async Task<List<Turno>> GetTurnos()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;

            try
            {
                using var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync(_Url + "ListarTurnosPendientes");


                var content = await response.Content.ReadAsStringAsync();

                var turnos = JsonSerializer.Deserialize<List<Turno>>(content);

                return turnos;

            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task<string> CancelarTurno(string turnoId)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;

            try
            {
                // Crea una instancia de HttpClient usando el HttpClientFactory
                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                // Construye la URL completa del endpoint
                string endpointUrl = _Url + "CancelarTurno?TurnoId=" + turnoId;

                // Realiza la solicitud HTTP PUT
                var response = await client.PutAsync(endpointUrl, null);
                // Lee el contenido de la respuesta como una cadena
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Devuelve la respuesta deserializada como una cadena
                    return responseContent;
                }
                else
                {
                    return responseContent;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
    }
}
