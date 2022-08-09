namespace ConsoleAppDbJokesBot;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class BotHandlers
{
    private static DbJokes _dbJokes = new DbJokes();
    
    public async static Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
        {
            return;
        }

        if (message.Text is not { } messageText)
        {
            return;
        }

        string response = "Неизвестная команда (для получения новой шутки введите /joke)";

        if (messageText == "/menu")
        {
            response = _dbJokes.GetRandomJoke();
        }

        Message sentMessage = await botClient.SendTextMessageAsync
        (
            chatId: message.Chat.Id,
            text: response,
            cancellationToken: cancellationToken
        );
    }


    public static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}