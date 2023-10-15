using Microsoft.Extensions.Logging;
using Proyecto2Maui.Data;
using Proyecto2Maui.Servicios;
using Radzen;

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
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<WeatherForecastService>();
            //Injecte el servicio para poder usarlo en toda mi aplicacion.
            builder.Services.AddSingleton<IValidadorCedula, ValidadorCedula>();
            builder.Services.AddSingleton<ITurnoServices, TurnoService>();

            builder.Services.AddRadzenComponents();

            return builder.Build();
        }
    }
}