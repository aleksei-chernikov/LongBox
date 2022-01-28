using LongBox.Telegram.Domain.Commands;
using LongBox.Telegram.Domain.Implementation.Extensions;
using Otreya.Common.CQRS;
using Otreya.Common.CQRS.Handlers;
using Telegram.Bot;

namespace LongBox.Telegram.Domain.Implementation.Handlers;

internal sealed class UpdateReceivedHandler : IVoidCommandHandler<UpdateReceivedCommand>
{
	private readonly ITelegramBotClient _botClient;
	private readonly ICommandExecutor _commandExecutor;

	public UpdateReceivedHandler(ICommandExecutor commandExecutor, ITelegramBotClient botClient)
	{
		_commandExecutor = commandExecutor;
		_botClient = botClient;
	}

	public async Task ExecuteAsync(UpdateReceivedCommand cmd, CancellationToken cancellationToken = default)
	{
		// TODO: Добавить обвязку на 1 одновременный запрос от одного пользователя (Redis например)

		var update = cmd.Update;
		var chatId = update.GetChatId();

		try
		{
			
		}
		catch
		{
			if (chatId != null)
				await _botClient.SendTextMessageAsync(chatId, "Извините, что-то пошло не так... Попробуйте позднее...", cancellationToken: cancellationToken);
		}
		
		
	}
}