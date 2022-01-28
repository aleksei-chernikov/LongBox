using Otreya.Common.CQRS.Middleware;

namespace Otreya.Common.CQRS.Implementation.Net.Builder;

public class CqrsBuilder
{
	private readonly List<Type> _middlewares = new List<Type>();
	private readonly Dictionary<Type, Type> _commandHandlers = new Dictionary<Type, Type>();

	public CqrsBuilder UseMiddleware<TMiddleware>()
		where TMiddleware: HandlerMiddleware
	{
		_middlewares.Add(typeof(TMiddleware));
		return this;
	}

	internal IEnumerable<Type> GetMiddlewares() => _middlewares;

	public void AddCommandHandler(Type commandType, Type commandHandlerType) =>
		_commandHandlers.Add(commandType, commandHandlerType);

	public Type? GetCommandHandler(Type commandType) =>
		_commandHandlers.ContainsKey(commandType) ? _commandHandlers[commandType] : null;

}