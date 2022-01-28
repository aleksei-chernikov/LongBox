using Telegram.Bot.Types.Enums;

namespace LongBox.Telegram.Domain.Exceptions;

public sealed class UnsupportedMessageType : Exception
{
	public UnsupportedMessageType(UpdateType updateType) : base($"Неподдерживаемый тип сообщения: {updateType}")
	{ }
}