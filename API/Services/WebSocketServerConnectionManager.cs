using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace API.WebSocketManager
{
public class WebSocketServerConnectionManager : IWebSocketManager
    {
        //dicionário para gerir id/websocket
        //private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string,WebSocket>();
        // id,socket
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

        public async Task SendMessageAsync(string message, string user) //para enviar mensagem para o cliente
        {
            if(_sockets.ContainsKey(user)) //tem o user no dic
            {
                Console.WriteLine("Targeted");
               
                if ( _sockets.TryGetValue(user, out WebSocket socket))
                {
                    if (socket.State == WebSocketState.Open)  //mandar mensagem para User especificao
                        await socket.SendAsync(
                            Encoding.UTF8.GetBytes(message),
                            WebSocketMessageType.Text,
                            true,
                            CancellationToken.None
                        );
                }
                else  //falha o Server não tem ID para o qual queremos enviar
                {
                    Console.WriteLine("Invalid Recipient");
                }
            }
            else                                                       
            {   
                Console.WriteLine("Invalid Recipient");
            }
        }
    
    }
}