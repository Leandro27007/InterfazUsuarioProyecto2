using DTOs;
using Newtonsoft.Json;

namespace Proyecto2Maui.Servicios
{
    public class MedicoService : IMedicoService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://proyecto2web.azurewebsites.net/api/Medico/";
        public MedicoService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<PacienteDTO> BuscarPaciente(string idRecibo)
        {

            try
            {
                // Crea una instancia de HttpClient utilizando la fábrica
                var httpClient = _httpClientFactory.CreateClient();

                // Construye la URL completa para la solicitud GET
                string apiUrl = $"{_Url}BuscarPacientePorIdRecibo {idRecibo}";

                // Realiza la solicitud GET
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var paciente = JsonConvert.DeserializeObject<PacienteDTO>(content);
                    return paciente;
                }
                else
                {

                    throw new Exception($"Error al obtener el paciente. Estado de respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CambiarEstadoRecibo(string idRecibo, string estado)
        {
            try
            {
                // Crea una instancia de HttpClient utilizando la fábrica
                var httpClient = _httpClientFactory.CreateClient();

                // Construye la URL completa para la solicitud POST
                string apiUrl = $"{_Url}CambiarEstadoRecibo";

                // Crea un objeto HttpRequestMessage para especificar la solicitud
                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);

                // Agrega los valores a los encabezados
                request.Headers.Add("idRecibo", idRecibo);
                request.Headers.Add("nuevoEstado", estado);

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
                // Manejo de errores generales
                // Aquí puedes registrar el error o tomar medidas específicas
                throw ex;
            }
        }

        public async Task<List<string>> ObtenerEstadosRecibo()
        {
            try
            {
                // Crea una instancia de HttpClient utilizando la fábrica
                var httpClient = _httpClientFactory.CreateClient();

                // Construye la URL completa para la solicitud GET
                string apiUrl = $"{_Url}EstadosDeRecibo";

                // Realiza la solicitud GET
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta y deserialízalo en una lista de cadenas
                    var content = await response.Content.ReadAsStringAsync();
                    var estadosRecibo = JsonConvert.DeserializeObject<List<string>>(content);
                    return estadosRecibo;
                }
                else
                {
                    // Si la respuesta no es exitosa, maneja el error según tus necesidades
                    // Puedes lanzar una excepción, registrar el error, o tomar otra acción.
                    throw new Exception($"Error al obtener estados de recibo. Estado de respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                // Aquí puedes registrar el error o tomar medidas específicas
                throw ex;
            }
        }

        public async Task<List<PacienteDTO>> ObtenerPacientesPendientes()
        {
            try
            {
                // Crea una instancia de HttpClient utilizando la fábrica
                var httpClient = _httpClientFactory.CreateClient();

                // Construye la URL completa para la solicitud GET
                string apiUrl = $"{_Url}PacientesPendientes";

                // Realiza la solicitud GET
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta y deserialízalo en una lista de objetos PacienteDTO
                    var content = await response.Content.ReadAsStringAsync();
                    var pacientesPendientes = JsonConvert.DeserializeObject<List<PacienteDTO>>(content);
                    return pacientesPendientes;
                }
                else
                {
                    // Si la respuesta no es exitosa, maneja el error según tus necesidades
                    // Puedes lanzar una excepción, registrar el error, o tomar otra acción.
                    throw new Exception($"Error al obtener pacientes pendientes. Estado de respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                // Aquí puedes registrar el error o tomar medidas específicas
                throw ex;
            }
        }
    }
}
