using Microsoft.AspNetCore.SignalR;

namespace webCollege.Chat;

public class ChatHub : Hub
{
    public async Task Send(string message, string userName)
    {
        await this.Clients.All.SendAsync("Receive", message, userName);
    }
}