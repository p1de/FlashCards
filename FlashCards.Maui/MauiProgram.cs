using FlashCards.Core.Application;
using FlashCards.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FlashCards.Maui;

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

        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("FlashCards.Maui.appsettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(config);

#if DEBUG
        builder.Services.AddLogging(
            logging =>
            {
                logging.AddDebug();
            });
#endif

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();
        builder.Services.AddManagers();
        builder.Services.AddPages();

        return builder.Build();
    }
}