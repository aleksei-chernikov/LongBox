using PipelineNet.Middleware;

namespace Otreya.Common.CQRS.Middleware;

public abstract class HandlerMiddleware : IAsyncMiddleware<HandlerContext>
{
	public abstract Task ExecuteAsync(HandlerContext handlerContext, Func<HandlerContext, Task> next);

	public Task Run(HandlerContext parameter, Func<HandlerContext, Task> next)
		=> ExecuteAsync(parameter, next);
}