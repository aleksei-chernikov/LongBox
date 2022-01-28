using Otreya.Common.CQRS.Implementation.Net.Builder;
using Otreya.Common.CQRS.Middleware;

namespace Otreya.Common.CQRS.Implementation.Net.DefaultMiddlewares;

public class DefaultCommandExecutionMiddleware : HandlerMiddleware
{

	private readonly IServiceProvider _serviceProvider;
	private readonly CqrsBuilder _cqrsBuilder;
	
	public DefaultCommandExecutionMiddleware(IServiceProvider serviceProvider, CqrsBuilder cqrsBuilder)
	{
		_serviceProvider = serviceProvider;
		_cqrsBuilder = cqrsBuilder;
	}

	public override async Task ExecuteAsync(HandlerContext handlerContext, Func<HandlerContext, Task> next)
	{
		var handlerType = _cqrsBuilder.GetCommandHandler(handlerContext.CommandType);
		if (handlerType == null)
			throw new ArgumentException("handlerType",
											$"Not found handler for command {handlerContext.CommandType.Name}");

		var commandHandler = _serviceProvider.GetService(handlerType);

		if (commandHandler == null)
			throw new ArgumentException("commandHandler",
										$"Not found handler for command {handlerContext.CommandType.Name} in DI container");
		
		var method = handlerType.GetMethod("ExecuteAsync");
		if (method == null)
			throw new ArgumentException("method", $"Not found method 'ExecuteAsync' on ${handlerType.Name}");
		
		var task = (Task) method.Invoke(commandHandler, new[] { handlerContext.Command, handlerContext.CancellationToken })!;
		await task.ConfigureAwait(false);

		if (handlerType.GetInterfaces().Any(t => t == typeof(IVoidCommand)))
		{
			handlerContext.SetVoidResult();
			return;
		}
		
		var result = task.GetType().GetProperty("Result")!;

		handlerContext.SetResult(result.GetValue(task));
	}
}