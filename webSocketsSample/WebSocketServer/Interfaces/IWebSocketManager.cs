using System.Net.WebSockets;

namespace WebSocketServer.WebSocketManager
{
    public interface IWebSocketManager
    {
        void AddSocket(WebSocket socket, string user);
    }
}