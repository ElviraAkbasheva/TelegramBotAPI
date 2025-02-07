using System.Net.Http.Json;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBotAPI
{
    public class UpdateHandler : IUpdateHandler
    {
        public delegate void MessageHandler(string message);
        public event MessageHandler? OnHandleUpdateStarted;
        public event MessageHandler? OnHandleUpdateCompleted;
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {

        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string message = update.Message.Text;
            OnHandleUpdateStarted?.Invoke(message);
            await botClient.SendMessage(update.Message.Chat.Id, "Сообщение успешно принято", cancellationToken: cancellationToken);
            if (message == "/cat")
            {
                var catFact = new CatFact();
                var fact = await catFact.GetCatFactAsync(cancellationToken);
                await botClient.SendMessage(update.Message.Chat.Id, fact, cancellationToken: cancellationToken);
            }
            OnHandleUpdateCompleted?.Invoke(message);
        }
    }
}
