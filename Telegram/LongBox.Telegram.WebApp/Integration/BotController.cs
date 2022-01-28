using LongBox.Telegram.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Otreya.Common.CQRS;
using Telegram.Bot.Types;

namespace LongBox.Telegram.WebApp.Integration;

[ApiController]
[Route("bot")]
public class BotController : ControllerBase
{
	private readonly BotConfiguration _botConfiguration;
	private readonly ICommandExecutor _commandExecutor;
	
	public BotController(BotConfiguration botConfiguration, ICommandExecutor commandExecutor)
	{
		_botConfiguration = botConfiguration;
		_commandExecutor = commandExecutor;
	}

	/// <summary>
	/// Entrypoint for telegram webhook
	/// </summary>
	[HttpPost]
	[Route("{token}")]
	public async Task<IActionResult> Post(string token)
	{
		if (token != _botConfiguration.BotToken)
			return BadRequest("А вы хто?! Мы вас не звали...");
		
		var update = await ReadBodyContent(Request.Body);
		await _commandExecutor.ExecuteVoidAsync(new UpdateReceivedCommand(update));
		
		return Ok();
	}

	/// <summary>
	/// Read body with Newtonsoft.Json serializer
	/// (for correct work with Telegram.Bot library)
	/// </summary>
	private async Task<Update> ReadBodyContent(Stream requestBody)
	{
		using var streamReader = new StreamReader(requestBody);
		var jsonString = await streamReader.ReadToEndAsync();

		var result = JsonConvert.DeserializeObject<Update>(jsonString);
		return result;
	}
	
}