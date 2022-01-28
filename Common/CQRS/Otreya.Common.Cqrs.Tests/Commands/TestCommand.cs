using System.Threading;
using System.Threading.Tasks;
using Otreya.Common.CQRS;
using Otreya.Common.CQRS.Handlers;

namespace Otreya.Common.Cqrs.Tests.Commands;

internal sealed record TestCommand(string TestText) 
	: ICommand<string>;

internal sealed class TestCommandHandler : ICommandHandler<TestCommand, string>
{
	public Task<string> ExecuteAsync(TestCommand cmd, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(cmd.TestText);
	}
}