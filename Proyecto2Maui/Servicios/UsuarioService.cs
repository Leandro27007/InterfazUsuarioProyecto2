using DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Proyecto2Maui.Modelos;
using System.Net.Http.Headers;
using System.Text;

namespace Proyecto2Maui.Servicios
{
    public class UsuarioService : IUsuarioServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _Url = "https://proyecto2web.azurewebsites.net/api/Usuario/";
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public UsuarioService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpClientFactory = httpClientFactory;
            this._authenticationStateProvider = authenticationStateProvider;

        }


        public async Task<UsuarioDTO> BuscarUsuario(string Cedula)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;

            throw new NotImplementedException();
        }

        public async Task<UsuarioDTO> CrearUsuario(UsuarioCreacionDTO usuarioCreacion)
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
                string apiUrl = $"{_Url}CrearUsuario";

                // Serializa el objeto CreacionPruebaLabDTO a JSON
                var jsonContent = JsonConvert.SerializeObject(usuarioCreacion);

                // Configura el contenido de la solicitud
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Realiza la solicitud POST
                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta y deserialízalo en un objeto PruebaLab
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var usuario = JsonConvert.DeserializeObject<UsuarioDTO>(responseContent);
                    return usuario;
                }
                else
                {
                    // Si la respuesta no es exitosa, maneja el error según tus necesidades
                    // Puedes lanzar una excepción, registrar el error, o tomar otra acción.
                    throw new Exception($"Error al crear el usuario. Estado de respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Editar(UsuarioEdicionDTO usuarioEdicion)
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
                string apiUrl = $"{_Url}EditarUsuario";

                // Serializa el objeto EdicionPruebaLabDTO a JSON
                var jsonContent = JsonConvert.SerializeObject(usuarioEdicion);

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
                throw;
            }
        }

        public async Task<bool> Eliminar(string cedula)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;

            throw new NotImplementedException();
        }

        public async Task<List<UsuarioDTO>> ObtenerUsuarios()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var accessTokenClaim = user.FindFirst("access_token");
            var accessToken = accessTokenClaim.Value!;


            try
            {
                using var client = _httpClientFactory.CreateClient();


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var respuesta = await client.GetAsync(this._Url + "ListarUsuario");

                var contenido = await respuesta.Content.ReadAsStringAsync();

                var usuario = System.Text.Json.JsonSerializer.Deserialize<List<UsuarioDTO>>(contenido);

                return usuario;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
