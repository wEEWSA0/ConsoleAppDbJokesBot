using ConsoleAppDbJokesBot;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var botClient = new TelegramBotClient("5502052554:AAFCcXDu3aBnrXyQ-vjGEyKjqOh3mHE_-7o");

using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(
    updateHandler: BotHandlers.HandleUpdateAsync,
    pollingErrorHandler: BotHandlers.HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine("Bot started");
if (me != null) Console.WriteLine($"Start listening for @{me.Username}");
Console.WriteLine("Press enter for stop");
Console.ReadKey();

cts.Cancel();

Console.WriteLine("Bot stopped");