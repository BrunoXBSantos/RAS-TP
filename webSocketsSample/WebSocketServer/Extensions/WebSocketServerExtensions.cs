using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebSocketServer.WebSocketManager;
using WebSocketServer.Middleware;

namespace WebSocketServer.Extension
{
public static class WebSocketServerExtensions
    {
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketServerMiddleware>();
        }

        public static IServiceCollection AddWebSocketServerConnectionManager(
            this IServiceCollection services
        )
        {
            services.AddSingleton<WebSocketServerConnectionManager>();
            return services;
        }
    }
}