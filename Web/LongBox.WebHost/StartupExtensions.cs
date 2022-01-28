using LongBox.Telegram.WebApp.Integration;
using LongBox.WebApp.Controllers;
using Otreya.Common.CQRS.Implementation.Net;
using Otreya.Common.Swagger;
using Telegram.Bot;

namespace LongBox.WebHost;

public static class StartupExtensions
{
	public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddControllers()
			.AddApplicationPart(typeof(TestController).Assembly)
			.AddApplicationPart(typeof(BotController).Assembly);
		
		services.AddDefaultCqrs("LongBox")
			.AddDefaultSwagger("LongBox.WebApp.xml", 
								"LongBox.Telegram.WebApp.xml", 
								"LongBox.Telegram.Domain.xml")
			.AddTelegramClient(configuration);
		
		return services;
	}

	public static IServiceCollection AddTelegramClient(this IServiceCollection services, IConfiguration configuration)
	{
		var botConfig = configuration.GetSection("TelegramBot").Get<BotConfiguration>();
		services.AddSingleton(botConfig);

		services.AddTransient<ITelegramBotClient, TelegramBotClient>(s => new TelegramBotClient(botConfig.BotToken));

		services.AddHostedService<ConfigureWebhook>();

		return services;
	}
}