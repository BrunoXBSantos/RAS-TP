using System;
using System.Collections.Generic;
using System.Net.WebSockets;


namespace WebSocketServer.WebSocketManager
{
public class WebSocketServerConnectionManager : IWebSocketManager
    {
        //dicionário para gerir id/websocket
        //private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string,WebSocket>();
        IDictionary<string, WebSocket> _sockets = new Dictionary<string, WebSocket>();
        public void AddSocket(WebSocket socket, string user)
        {
            //string ConnID = Guid.NewGuid().ToString(); //criação do id
            _sockets.Add(user, socket);
            Console.WriteLine(
                "WebSocketServerConnectionManager-> AddSocket: WebSocket added with ID: " + user
            );
            //return ConnID;
        }

        public IDictionary<string, WebSocket> GetAllSockets()
        {
            return _sockets;
        }

       
    }
}
