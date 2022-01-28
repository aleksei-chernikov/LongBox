using System.Threading;
using System.Threading.Tasks;
using Otreya.Common.CQRS;
using Otreya.Common.CQRS.Handlers;

namespace Otreya.Common.Cqrs.Tests.Commands;

public sealed record TestQuery(string Test) : IQuery<string>;

internal sealed class TestQueryHandler : IQueryHandler<TestQuery, string>
{
	public Task<string> ExecuteAsync(TestQuery query, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(query.Test);
	}
}