using System.Net.WebSockets;
using System.Text;
using API.WebSocketManager;

namespace API.Middleware
{
    public class WebSocketServerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly WebSocketServerConnectionManager _manager;

        public WebSocketServerMiddleware(RequestDelegate next, WebSocketServerConnectionManager manager)
        {
            _next = next;
            _manager = manager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                //aqui fazer cena de tirar parametro
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                //webSocket.
                
                //_manager.AddSocket(webSocket, user);
                
                //Console.WriteLine(webSocket.ToString());

                //await SendConnIDAsync(webSocket, ConnID); //envia o id ao cliente

                Console.WriteLine("WebSocket Connected");

                await Receive(
                    webSocket,
                    async (result, buffer) =>
                    {
                        
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            Console.WriteLine($"Receive->Text");
                            Console.WriteLine($"Message: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                            _manager.AddSocket(webSocket, Encoding.UTF8.GetString(buffer, 0, result.Count));//adicionada ao dicionário
                            //await RouteJSONMessageAsync(Encoding.UTF8.GetString(buffer, 0, result.Count));
                            await SendMessageAsync("olá", Encoding.UTF8.GetString(buffer, 0, result.Count));
                            return;
                        }
                        else
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            string id =_manager.GetAllSockets().FirstOrDefault(s => s.Value == webSocket).Key;

                            Console.WriteLine($"Receive->Close");

                            //_manager.GetAllSockets().Remove(id, out WebSocket sock);
                            _manager.GetAllSockets().Remove(id, out WebSocket sock);
                            Console.WriteLine(
                                "Managed Connections: " + _manager.GetAllSockets().Count.ToString()
                            );

                            await sock.CloseAsync(
                                result.CloseStatus.Value,
                                result.CloseStatusDescription,
                                CancellationToken.None
                            );

                            return;
                        }
                    }
                );
            }
            else
            {
                await _next(context);
            }
        }

        private async Task Receive(WebSocket socket,Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(
                    buffer: new ArraySegment<byte>(buffer),
                    cancellationToken: CancellationToken.None
                );

                handleMessage(result, buffer);
            }
        }

 public async Task SendMessageAsync(string message, string user) //para enviar mensagem para o cliente
        {
            if(_manager.GetAllSockets().ContainsKey(user)) //tem o user no dic
            {
                Console.WriteLine("Targeted");
               
                if ( _manager.GetAllSockets().TryGetValue(user, out WebSocket socket))
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

