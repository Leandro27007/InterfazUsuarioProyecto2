using DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Proyecto2Maui.Servicios
{
    public class ReporteService : IReporteService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://proyecto2web.azurewebsites.net/api/Reporte/";
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public ReporteService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpClientFactory = httpClientFactory;
            this._authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ReporteDTO> ObtenerReporte(string FechaInicio, string FechaFinal)
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


                var respuesta = await client.GetAsync(_Url + $"ListarPruebas?FechaInicio={FechaInicio}&FechaFinal={FechaFinal}");

                respuesta.EnsureSuccessStatusCode();

                var respuestaARetornar = await respuesta.Content.ReadAsStringAsync();

                var response = JsonSerializer.Deserialize<ReporteDTO>(respuestaARetornar);

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
