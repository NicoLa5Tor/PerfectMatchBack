using Microsoft.AspNetCore.SignalR;

namespace PerfectMatchBack.Services.Implementation
{
    public class EmailHub:Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
