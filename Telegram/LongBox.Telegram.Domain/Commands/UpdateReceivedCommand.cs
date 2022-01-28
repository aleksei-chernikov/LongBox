using Otreya.Common.CQRS;
using Telegram.Bot.Types;

namespace LongBox.Telegram.Domain.Commands;

public sealed record UpdateReceivedCommand(Update Update) 
	: IVoidCommand;