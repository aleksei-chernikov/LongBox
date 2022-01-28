using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Otreya.Common.Swagger;

public static class StartupExtensions
{
	/// <summary>
	/// Регистрирует сервисы Swagger-а
	/// </summary>
	/// <param name="services">Сервисы приложения</param>
	/// <param name="documentName">Имя документа Swagger-а</param>
	/// <param name="title">Заголовок документа Swagger-а</param>
	/// <param name="version">Версия документа Swagger-а</param>
	/// <param name="xmlDocFiles">Файлы с xml документацией</param>
	public static IServiceCollection AddDefaultSwagger(this IServiceCollection services,  
														params string[] xmlDocFiles)
	{
		services.AddSwaggerGen(c =>
		{
			var basePath = PlatformServices.Default.Application.ApplicationBasePath;
			foreach (var xmlDocFile in xmlDocFiles)
			{
				var xmlPath = Path.Combine(basePath, xmlDocFile);
				c.IncludeXmlComments(xmlPath, true);
			}
			
			c.CustomSchemaIds(x => x.FullName);
		});

		return services;
	}

	public static IApplicationBuilder UseDefaultSwagger(this IApplicationBuilder applicationBuilder)
	{
		applicationBuilder.UseSwagger();
		applicationBuilder.UseSwaggerUI();

		return applicationBuilder;
	}
}