using System.Net.WebSockets;
using System.Text;

namespace Fish.Services;

public class FishesWebSocketsMiddleware
{
    private readonly RequestDelegate _next;

    public FishesWebSocketsMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine(context.Request.Protocol);
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            //var buffer = new byte[1024 * 4];
            //var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), context.RequestAborted);
            while (!context.RequestAborted.IsCancellationRequested)
            {
                //Console.WriteLine($"client: {Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, result.Count))}");
                await webSocket.SendAsync(
                    new ArraySegment<byte>(Encoding.UTF8.GetBytes(Aquarium.GetAllJson())),
                    WebSocketMessageType.Text,
                    true,
                    context.RequestAborted);
                //result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), context.RequestAborted);
                await Task.Delay(1000);
            }

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, context.RequestAborted);
        }
        else
            await _next(context);
    }
}