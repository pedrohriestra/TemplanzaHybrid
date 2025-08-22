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
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IUsuariosService, UsuariosService>();
            builder.Services.AddSingleton<IBlendsService, BlendsService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IConfirmService, ConfirmService>();
            builder.Services.AddSingleton<IStaticAssetsService, StaticAssetsService>();

            return builder.Build();
        }
    }
}
