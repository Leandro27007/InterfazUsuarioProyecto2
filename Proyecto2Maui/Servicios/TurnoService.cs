using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Proyecto2Maui.Modelos;
using Proyecto2Maui.Servicios;

namespace Proyecto2Maui.Servicios
{
    public class TurnoService : ITurnoServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://localhost:7290/api/Turno/ListarTurnosPendientes";
        public TurnoService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<List<Turno>> GetTunos()
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();

                var response = await client.GetAsync(_Url);


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
