using System.Net.WebSockets;

namespace API.WebSocketManager
{
    public interface IWebSocketManager
    {
        void AddSocket(WebSocket socket, string user);

        IDictionary<string, WebSocket> GetAllSockets();
        Task SendMessageAsync(string message, string user);
    }
}