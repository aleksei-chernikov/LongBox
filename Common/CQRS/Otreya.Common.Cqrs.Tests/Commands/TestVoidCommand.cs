using System;
using System.Threading;
using System.Threading.Tasks;
using Otreya.Common.CQRS;
using Otreya.Common.CQRS.Handlers;

namespace Otreya.Common.Cqrs.Tests.Commands;

public sealed record TestVoidCommand(string TestString) : IVoidCommand;

internal sealed class TestVoidCommandHandler : IVoidCommandHandler<TestVoidCommand>
{
	public Task ExecuteAsync(TestVoidCommand cmd, CancellationToken cancellationToken = default)
	{
		Console.WriteLine(cmd);
		return Task.CompletedTask;
	}
}