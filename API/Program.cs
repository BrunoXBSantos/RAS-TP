using API.BackgroundTask;
using API.Data;
using API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try 
            {
                var context = services.GetRequiredService<DataContext>();
                var UserManager = services.GetRequiredService<UserManager<AppUser>>();
                // var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                await context.Database.MigrateAsync();
                await Seeder.Seed(UserManager/*, RoleManager*/, context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices(services => 
                    services.AddHostedService<Background>());
    }
}
