using Otreya.Common.CQRS.Implementation.Net.Builder;
using Otreya.Common.CQRS.Middleware;
using PipelineNet.Pipelines;

namespace Otreya.Common.CQRS.Implementation.Net;

public class CommandExecutor : ICommandExecutor
{
	private readonly List<Type> _middlewares;
	private readonly IAsyncPipeline<HandlerContext> _asyncPipeline;

	public CommandExecutor(CqrsBuilder builder, IAsyncPipeline<HandlerContext> asyncPipeline)
	{
		_asyncPipeline = asyncPipeline;
		_middlewares = builder.GetMiddlewares().ToList();
	}

	private IAsyncPipeline<HandlerContext> GetExecutionPipeline()
	{
		foreach (var middleware in _middlewares)
		{
			_asyncPipeline.Add(middleware);
		}

		return _asyncPipeline;
	}
	
	public async Task<TResult?> ExecuteAsync<TResult>(ICommand<TResult> cmd, CancellationToken cancellationToken = default)
		where TResult: class
	{
		var handlerContext = new HandlerContext
		{
			Command = cmd,
			CommandType = cmd.GetType(),
			ResultType = typeof(TResult),
			CancellationToken = cancellationToken
		};

		var pipeline = GetExecutionPipeline();

		await pipeline.Execute(handlerContext);
		
		return handlerContext.Result as TResult;
	}

	public async Task ExecuteVoidAsync(IVoidCommand cmd, CancellationToken cancellationToken = default)
	{
		var handlerContext = new HandlerContext
		{
			Command = cmd,
			CommandType = cmd.GetType(),
			ResultType = null,
			CancellationToken = cancellationToken
		};

		var pipeline = GetExecutionPipeline();

		await pipeline.Execute(handlerContext);
	}

	public async Task<TResult?> ExecuteAsync<TResult>(IQuery<TResult> cmd, CancellationToken cancellationToken = default)
		where TResult: class
	{
		var handlerContext = new HandlerContext
		{
			Command = cmd,
			CommandType = cmd.GetType(),
			ResultType = typeof(TResult),
			CancellationToken = cancellationToken
		};

		var pipeline = GetExecutionPipeline();

		await pipeline.Execute(handlerContext);

		return handlerContext.Result as TResult;
	}
}