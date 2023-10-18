using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Proyecto2Maui.Modelos;
using Proyecto2Maui.Modelos.Request;
using Proyecto2Maui.Servicios;
using Radzen;

namespace Proyecto2Maui.Servicios
{
    public class TurnoService : ITurnoServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://localhost:7290/api/Turno/";
        public TurnoService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<string> GenerarTurno(GenerarTurnoRequest turnoParaGenerar)
        {

            try
            {
                using var client = _httpClientFactory.CreateClient();

                var turnoJson = JsonSerializer.Serialize(turnoParaGenerar);

                var contenido = new StringContent(turnoJson, Encoding.UTF8, "application/json");

                var respuesta = await client.PostAsync(_Url+ "GenerarTurno", contenido);

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
            try
            {
                using var client = _httpClientFactory.CreateClient();

                var response = await client.GetAsync(_Url+ "ListarTurnosPendientes");


                var content = await response.Content.ReadAsStringAsync();

                var turnos = JsonSerializer.Deserialize<List<Turno>>(content);

                return turnos;

            }
            catch (Exception ex)
            {

                throw;
            }


        }
    }
}
