using System.Threading.Tasks;
using Otreya.Common.CQRS;
using Otreya.Common.Cqrs.Tests.Commands;
using Xunit;

namespace Otreya.Common.Cqrs.Tests;

public class CommandExecutorTests
{
	private readonly ICommandExecutor _commandExecutor;
	
	public CommandExecutorTests(ICommandExecutor commandExecutor)
	{
		_commandExecutor = commandExecutor;
	}
	
	[Fact]
	public async Task ExecuteCommandAsync()
	{
		var cmd = new TestCommand("Tust");
		var result = await _commandExecutor.ExecuteAsync(cmd);

		Assert.True(result == cmd.TestText);
	}

	[Fact]
	public async Task ExecuteVoidCommandAsync()
	{
		await _commandExecutor.ExecuteVoidAsync(new TestVoidCommand("Tust"));
	}

	[Fact]
	public async Task ExecuteQueryAsync()
	{
		var cmd = new TestQuery("Tust");
		var result = await _commandExecutor.ExecuteAsync(cmd);
		
		Assert.True(cmd.Test == result);
	}
	
}