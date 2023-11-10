using Microsoft.JSInterop;

namespace Proyecto2Maui.Helpers
{
    public static class IJSExtensions
    {
        public static async Task<object> GuardarComo(this IJSRuntime js, string nombreArchivo, byte[] archivo)
        {
            return  js.InvokeAsync<object>("saveAsFile", nombreArchivo, Convert.ToBase64String(archivo));
        }

    }
}
