using Telegram.Bot;

namespace webCollege;

public class Bot
{
    private static TelegramBotClient _client;
    private static IConfiguration _config;

    public Bot(IConfiguration config)
    {
        _config = config;
    }
    public static TelegramBotClient GetTelegramBot()
    {
        if (_client != null)
        {
            return _client;
        }

        var botToken = _config["Telegram:BotToken"];
        _client = new TelegramBotClient(botToken);
        return _client;
    }
}