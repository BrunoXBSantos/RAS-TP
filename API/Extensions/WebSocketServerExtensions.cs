using API.Middleware;
using API.WebSocketManager;

namespace API.Extensions
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