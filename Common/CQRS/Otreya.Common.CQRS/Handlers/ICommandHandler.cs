namespace Otreya.Common.CQRS.Handlers;

public interface ICommandHandler<in TCommand, TResult>
	where TCommand : ICommand<TResult> 
	where TResult: class
{
	Task<TResult> ExecuteAsync(TCommand cmd, CancellationToken cancellationToken = default);
}