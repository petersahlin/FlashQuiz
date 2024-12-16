using Microsoft.Extensions.Logging;
using TriviaApp.Pages;
using TriviaApp.Services;
using TriviaApp.ViewModels;

namespace TriviaApp
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


            builder.Services.AddSingleton<PlayPage>();
            builder.Services.AddSingleton<PlayPageViewModel>();
            builder.Services.AddTransient<TriviaPage>();
            builder.Services.AddTransient<TriviaViewModel>();
            builder.Services.AddTransient<ITriviaService, TriviaService>();
            builder.Services.AddTransient<IAlertService, AlertService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
