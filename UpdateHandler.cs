using System.Net.Http.Json;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBotAPI
{
    public class UpdateHandler : IUpdateHandler
    {
        public delegate void MessageHandler(string message);
        public event MessageHandler OnHandleUpdateStarted;
        public event MessageHandler OnHandleUpdateCompleted;
        record CatFactDto(string Fact, int Length);
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {

        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string https_adress = "https://catfact.ninja/fact";
            string message = update.Message.Text;
            OnHandleUpdateStarted(message);
            await botClient.SendMessage(update.Message.Chat.Id, "Сообщение успешно принято");
            if (message == "/cat")
            {
                try
                {
                    using var client = new HttpClient();
                    var catFact = await client.GetFromJsonAsync<CatFactDto>(https_adress, cancellationToken);
                    await botClient.SendMessage(update.Message.Chat.Id, catFact.Fact, cancellationToken: cancellationToken);
                }
                catch
                {
                    await botClient.SendMessage(update.Message.Chat.Id, $"Ресурс {https_adress} сейчас не доступен");
                }
            }
            OnHandleUpdateCompleted(message);
        }
    }
}
