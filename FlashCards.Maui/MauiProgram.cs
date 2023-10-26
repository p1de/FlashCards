using FlashCards.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        builder.Services.AddSingleton<IDbContext>(new MongoDbContext(builder.Configuration.GetConnectionString("MongoDbConnectionString")));

        return builder.Build();
	}
}
