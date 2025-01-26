using Microsoft.AspNetCore.Mvc;
using webCollege.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace webCollege.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private TelegramBotClient bot = Bot.GetTelegramBot();
        private long chatId = 6269041724;
        
        [HttpPost("create")]
        public async void Create(Client client)
        {
            await bot.SendTextMessageAsync(chatId, client.FormatMessage());
        }
        
        
    }
}

