using Telegram.Bot;

namespace webCollege;

public class Bot
{
    private static TelegramBotClient client { get; set; }

    public static TelegramBotClient GetTelegramBot()
    {
        if (client != null)
        {
            return client;
        }

        client = new TelegramBotClient("9549594343:AAEurlFLmwM4UjOggP2xJlEkCQ4DhTdCfwo");
        return client;
    }
}