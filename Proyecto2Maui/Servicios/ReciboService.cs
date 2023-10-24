using Proyecto2Maui.Modelos.Request;
using Proyecto2Maui.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto2Maui.Servicios
{
    public class ReciboService : IReciboService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://localhost:7290/api/Recibo/";
        public ReciboService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<ReciboResponse> GenerarRecibo(ReciboRequest recibo)
        {

            try
            {

                recibo.idCajero = "40200000000";

                using var client = _httpClientFactory.CreateClient();

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
