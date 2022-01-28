namespace Otreya.Common.CQRS.Middleware;

public class HandlerContext
{
	public Type CommandType { get; init; }

	public Type? ResultType { get; init; } 
	
	public object Command { get; init; }
	
	public object? Result { get; protected set; }
	
	public CancellationToken CancellationToken { get; init; }
	
	private Dictionary<string, object>? _customData;
	
	public Dictionary<string, object> CustomData
	{
		get
		{
			if (_customData != null)
				return _customData;

			_customData = new Dictionary<string, object>();
			return _customData;
		}
	}

	public void SetResult(object? result) => Result = result;

	public void SetVoidResult() => Result = VoidResult.Value;
}