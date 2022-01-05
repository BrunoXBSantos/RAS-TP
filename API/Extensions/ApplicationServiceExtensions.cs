using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.AppNetUsers;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services, IConfiguration config)
        {
            Services = AddInterfacesScopes(Services);

            Services.AddSingleton<IBettingApi, BettingApi>();
            Services.AddSingleton<IObservables, Observables>();
            Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            Services.AddDbContext<DataContext>(options =>
            {
                // pode receber 3 tipos de parametros, passar o rato para ve-los
                // mas usaremos o segundo
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return Services;
        }

        /*Nao posso injetar um scope em um singleton, mas o contraio sim*/

        public static IServiceCollection AddInterfacesScopes(this IServiceCollection Services)
        {
            //? Services.AddScoped<Interface Class,Main Class>();
            Services.AddScoped<IAppUserRepository, AppUserRepository>();
            Services.AddScoped<IEventRepository, EventRepository>();
            Services.AddScoped<IBetRepository, BetRepository>();
            Services.AddScoped<ITokenService, TokenService>();


            return Services;
        }
    }
}