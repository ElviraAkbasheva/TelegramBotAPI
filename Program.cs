using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;


namespace TelegramBotAPI
{
    internal class Program
    {
        static public void HandleUpdateStarted(string message)
        {
            Console.WriteLine($"Началась обработка сообщения {message}");
        }
        static public void HandleUpdateCompleted(string message)
        {
            Console.WriteLine($"Закончилась обработка сообщения {message}");
        }
        static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var messHandler = new UpdateHandler();
            messHandler.OnHandleUpdateStarted += HandleUpdateStarted;
            messHandler.OnHandleUpdateCompleted += HandleUpdateCompleted;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message],
                DropPendingUpdates = true
            };

            try
            {
                string token = Environment.GetEnvironmentVariable("HW_TG_TOKEN");
                var botClient = new TelegramBotClient(token);
                botClient.StartReceiving(messHandler, receiverOptions, cts.Token);

                var me = await botClient.GetMe();
                Console.WriteLine($"{me.FirstName} запущен");

                Console.WriteLine("Нажмите клавишу A для выхода");

                while (true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.A)
                    {
                        messHandler.OnHandleUpdateStarted -= HandleUpdateStarted;
                        messHandler.OnHandleUpdateCompleted -= HandleUpdateCompleted;
                        await cts.CancelAsync();
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Информация: {me}");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка в токене");
            }
        }
    }
}

