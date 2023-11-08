using Microsoft.AspNetCore.Components.Authorization;
using Proyecto2Maui.Modelos.Request;
using Proyecto2Maui.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Web.Http;

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
    }
}
