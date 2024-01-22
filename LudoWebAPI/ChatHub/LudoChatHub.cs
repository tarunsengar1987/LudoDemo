using LudoWebAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class LudoChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendPrivateMessage(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveMessage", message);
    }


}
