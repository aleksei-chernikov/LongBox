using LongBox.WebHost;
using Otreya.Common.Swagger;

var builder = WebApplication
	.CreateBuilder(args);

builder.Services
	.ConfigureServices(builder.Configuration);

var app = builder.Build();

app
	.UseDeveloperExceptionPage()
	.UseRouting()
	.UseDefaultSwagger()
	.UseEndpoints(cfg =>
	{
		cfg.MapControllers();
	});

app.Run();