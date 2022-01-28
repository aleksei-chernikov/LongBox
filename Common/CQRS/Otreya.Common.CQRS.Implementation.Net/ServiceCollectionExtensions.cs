using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Otreya.Common.CQRS.Handlers;
using Otreya.Common.CQRS.Implementation.Net.Builder;
using Otreya.Common.CQRS.Implementation.Net.DefaultMiddlewares;
using Otreya.Common.CQRS.Middleware;
using PipelineNet.MiddlewareResolver;
using PipelineNet.Pipelines;

namespace Otreya.Common.CQRS.Implementation.Net;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDefaultCqrs(this IServiceCollection services,
													string? assembliesStartWith = null,
													Action<CqrsBuilder>? cqrsConfiguration = null)
	{
		services.AddTransient<IMiddlewareResolver, MiddlewareResolver>();

		var builder = new CqrsBuilder();
		cqrsConfiguration?.Invoke(builder);
		builder.UseMiddleware<DefaultCommandExecutionMiddleware>();

		services.AddTransient<ICommandExecutor, CommandExecutor>();
		services.AddTransient<IMiddlewareResolver, MiddlewareResolver>();
		services.AddTransient<DefaultCommandExecutionMiddleware>();
		services.AddTransient<IAsyncPipeline<HandlerContext>, AsyncPipeline<HandlerContext>>();
		
		services.AddSingleton(builder);

		services.AddCqrsHandlers(builder, assembliesStartWith);

		return services;
	}

	private static void AddCqrsHandlers(this IServiceCollection services, CqrsBuilder builder, string? startWith)
	{
		var assembliesForAnalise = startWith == null 
			? AppDomain.CurrentDomain.GetAssemblies()
			: AppDomain.CurrentDomain.GetAssemblies()
				.Where(a => a.GetName().Name?.StartsWith(startWith) == true);

		var (commands, handlers) = GetCommandsAndHandlers(assembliesForAnalise);

		foreach (var commandType in commands)
		{
			handlers.TryGetValue(commandType, out var handlerType);
			if (handlerType == null)
			{
				Debug.WriteLine($"[{commandType.FullName}] Not found handlers");
				continue;
			}

			builder.AddCommandHandler(commandType, handlerType);
			services.AddTransient(handlerType);
		}
	}

	private static (HashSet<Type> commands, Dictionary<Type, Type> handlers) GetCommandsAndHandlers(IEnumerable<Assembly> assemblies)
	{
		var baseCommandInterfaces = new HashSet<Type>
		{
			typeof(ICommand<>),
			typeof(IQuery<>),
			typeof(IVoidCommand)
		};

		var baseCommandHandlerInterfaces = new HashSet<Type>
		{
			typeof(ICommandHandler<,>),
			typeof(IQueryHandler<,>),
			typeof(IVoidCommandHandler<>)
		};
			
		var commands = new HashSet<Type>();
		var commandHandlers = new Dictionary<Type, Type>();
		
		foreach (var assembly in assemblies)
		{
			var types = assembly.GetTypes();
			
			foreach (var type in types)
			{
				if (type.IsInterface)
					continue;
				
				var isCommand = type
					.GetInterfaces()
					.Any(t => (t.IsGenericType && baseCommandInterfaces.Contains(t.GetGenericTypeDefinition()))
							|| t == typeof(IVoidCommand));

				if (isCommand)
				{
					commands.Add(type);
					continue;
				}

				var commandHandlerType = type
					.GetInterfaces()
					.FirstOrDefault(t => t.IsGenericType && baseCommandHandlerInterfaces.Contains(t.GetGenericTypeDefinition()));
				
				if (commandHandlerType == null)
					continue;
				
				commandHandlers.Add(commandHandlerType.GenericTypeArguments[0], type);
			}
		}

		return (commands, commandHandlers);
	}
	
}