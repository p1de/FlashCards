using CommunityToolkit.Maui;
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
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var assembly = Assembly.GetExecutingAssembly();
        var appsettingsResourceName = "FlashCards.Maui.appsettings.json";
#if DEBUG
        appsettingsResourceName = "FlashCards.Maui.appsettings.Development.json";
#endif
        using var stream = assembly.GetManifestResourceStream(appsettingsResourceName);
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