namespace Otreya.Common.CQRS;

public interface ICommand<out TResponse>
	where TResponse: class { }