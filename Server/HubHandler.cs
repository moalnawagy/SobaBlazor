using Microsoft.AspNetCore.SignalR;

namespace SobaBlazor.Server;

public abstract class HubHandlerBase:Hub
{

    public abstract Task SendMessage();

}

public class HubHandler:HubHandlerBase 
{
    public override Task OnConnectedAsync()
    {
        Console.WriteLine("connected");
        Console.WriteLine(Context.ConnectionId);
        // Console.WriteLine(Context.ConnectionAborted.WaitHandle);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("Ending.....");
        Console.WriteLine(Context.GetHttpContext());
        return base.OnDisconnectedAsync(exception);
    }

    public override async Task SendMessage()
    {
        await Clients.All.SendAsync("ReceiveMessage", "hello");
    }
   
}
