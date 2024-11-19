using DotNetEnv;
using Lab9.Services;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using Mopups;
using Mopups.Hosting;

namespace Lab9
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            Env.Load();

            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .ConfigureMopups()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .Services.AddSingleton<StaffService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<LoginService>();
            return builder.Build();
        }
    }
}
