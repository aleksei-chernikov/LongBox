namespace Otreya.Common.CQRS;

public interface IQuery<out TResponse> : ICommand<TResponse>
	where TResponse: class {  }