using Microsoft.Extensions.Logging;
using Proyecto2Maui.Data;
using Proyecto2Maui.Servicios;

namespace Proyecto2Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();
            //Injecte el servicio para poder usarlo en toda mi aplicacion.
            builder.Services.AddSingleton<IValidadorCedula, ValidadorCedula>();



            return builder.Build();
        }
    }
}