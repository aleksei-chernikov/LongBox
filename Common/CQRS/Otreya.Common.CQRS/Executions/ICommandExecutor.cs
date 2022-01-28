namespace Otreya.Common.CQRS;

public interface ICommandExecutor
{

	Task<TResult?> ExecuteAsync<TResult>(ICommand<TResult> cmd, CancellationToken cancellationToken = default)
		where TResult: class;

	Task ExecuteVoidAsync(IVoidCommand cmd, CancellationToken cancellationToken = default);

	Task<TResult?> ExecuteAsync<TResult>(IQuery<TResult> cmd, CancellationToken cancellationToken = default)
		where TResult: class;

}