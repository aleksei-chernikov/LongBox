namespace Otreya.Common.CQRS.Handlers;

public interface IVoidCommandHandler<in TVoidCommand>
{
	Task ExecuteAsync(TVoidCommand cmd, CancellationToken cancellationToken = default);
}