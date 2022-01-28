using LongBox.Telegram.Domain.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LongBox.Telegram.Domain.Implementation.Extensions;

public static class UpdateExtensions
{
	public static long? GetChatId(this Update update)
	{
		switch (update.Type)
		{
			case UpdateType.Message:
				return update.Message!.Chat.Id;
			case UpdateType.CallbackQuery:
				return update.CallbackQuery!.From.Id;
			case UpdateType.EditedMessage:
				return update.EditedMessage!.Chat.Id;
			case UpdateType.ChannelPost:
				return update.ChannelPost!.Chat.Id;
			case UpdateType.EditedChannelPost:
				return update.EditedChannelPost!.Chat.Id;
			case UpdateType.MyChatMember:
				return update.MyChatMember!.Chat.Id;
			case UpdateType.ChatMember:
				return update.ChatMember!.Chat.Id;
			case UpdateType.ChatJoinRequest:
				return update.ChatMember!.Chat.Id;
			
			case UpdateType.Unknown:
			case UpdateType.InlineQuery:
			case UpdateType.ChosenInlineResult:
			case UpdateType.ShippingQuery:
			case UpdateType.PreCheckoutQuery:
			case UpdateType.Poll:
			case UpdateType.PollAnswer:
				throw new UnsupportedMessageType(update.Type);
				
			default:
				throw new ArgumentOutOfRangeException(nameof(update), "Неизвестный Update.Type от TG.");
		}
	}
	
}