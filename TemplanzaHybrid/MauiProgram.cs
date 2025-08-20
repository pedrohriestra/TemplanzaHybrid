using Microsoft.Extensions.Logging;
using TemplanzaHybrid.Services;

namespace TemplanzaHybrid
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            // Devtools y logging SOLO en Debug
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // DI
            builder.Services.AddSingleton<IUsuariosService, UsuariosService>();
            builder.Services.AddSingleton<IBlendsService, BlendsService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IConfirmService, ConfirmService>();

            return builder.Build();
        }
    }
}
