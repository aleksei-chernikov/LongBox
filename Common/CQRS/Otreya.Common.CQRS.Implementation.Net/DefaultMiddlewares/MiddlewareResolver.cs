using PipelineNet.MiddlewareResolver;

namespace Otreya.Common.CQRS.Implementation.Net.DefaultMiddlewares;

public class MiddlewareResolver : IMiddlewareResolver
{
	private readonly IServiceProvider _serviceProvider;

	public MiddlewareResolver(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public object Resolve(Type type)
	{
		var handlerMiddleware = _serviceProvider.GetService(type);
		if (handlerMiddleware == null)
			throw new ArgumentException("handlerMiddleware", $"Not found type {type.FullName} on DI");

		return handlerMiddleware;
	}
}