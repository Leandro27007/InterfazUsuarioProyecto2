using DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Proyecto2Maui.Modelos.Request;
using Proyecto2Maui.Modelos.Response;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Proyecto2Maui.Servicios
{
    public class ReciboService : IReciboService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://proyecto2web.azurewebsites.net/api/Recibo/";
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public ReciboService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpClientFactory = httpClientFactory;
            this._authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ReciboResponse> GenerarRecibo(ReciboRequest recibo)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;


            try
            {

                recibo.idCajero = "40200000000";

                using var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                var reciboJson = JsonSerializer.Serialize(recibo);

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var contenido = new StringContent(reciboJson, Encoding.UTF8, "application/json");

                var respuesta = await client.PostAsync(_Url + "GenerarRecibo", contenido);

                respuesta.EnsureSuccessStatusCode();

                var respuestaARetornar = await respuesta.Content.ReadAsStringAsync();

                var response = JsonSerializer.Deserialize<ReciboResponse>(respuestaARetornar);

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<ReciboDTO>> ListarRecibos()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;


            try
            {

                using var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                var respuesta = await client.GetAsync(_Url + "ListarRecibos?PaginaActual=1");

                respuesta.EnsureSuccessStatusCode();

                var respuestaARetornar = await respuesta.Content.ReadAsStringAsync();

                var response = JsonSerializer.Deserialize<List<ReciboDTO>>(respuestaARetornar);

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> Reembolsar(ReembolsoRequest reembolsoRequest)
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
                string apiUrl = $"{_Url}CancelarRecibo";

                // Crea un objeto HttpRequestMessage para especificar la solicitud
                var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, apiUrl);

                // Agrega los valores a los encabezados
                request.Headers.Add("IdRecibo", reembolsoRequest.IdRecibo);
                request.Headers.Add("notaModificacion", reembolsoRequest.notaModificacion);

                // Realiza la solicitud POST
                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    // La solicitud fue exitosa
                    return true;
                }
                else
                {
                    // Si la respuesta no es exitosa, maneja el error según tus necesidades
                    // Puedes lanzar una excepción, registrar el error, o tomar otra acción.
                    throw new Exception($"Error al cambiar el estado del recibo. Estado de respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
