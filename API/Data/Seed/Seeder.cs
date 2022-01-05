using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seeder
{

    public static async Task Seed(UserManager<AppUser> userManager, DataContext _context)
    {
        await SeedUsers(userManager, _context);
        //await Seed_Attachment(_context);
        //await Seed_Warnings_State(_context);

        #region Bet Secondary Tables
        await Seed_Tables<BetState>(_context, _context.DB_BetState, "Data/Seed/Bet_State/Bet_State.data.json");
        #endregion

        #region Bet Secondary Tables
        await Seed_Tables<EventState>(_context, _context.DB_EventState, "Data/Seed/Event_State/Event_State.data.json");
        #endregion

        #region Event Secondary Tables
        await Seed_Tables<EventType>(_context, _context.DB_EventType, "Data/Seed/Event_Type/Event_Type.data.json");
        #endregion

        #region Sport Secondary Tables
        await Seed_Tables<SportType>(_context, _context.DB_SportType, "Data/Seed/Sport_Type/Sport_Type.data.json");
        await Seed_Tables<Sport>(_context, _context.DB_Sports, "Data/Seed/Sport/Sport.data.json");
        #endregion

        // await Seed_Tables<Server>(_context, _context.DB_Server, "Data/Seed/Server/Server.data.json");
        // await Seed_Warning(_context, "Data/Seed/Warnings/Warnings.data.json");

        // await Seed_Tables<ServerComment>(_context, _context.DB_Server_Comment, "Data/Seed/Server_Comment/Server_Comment.data.json");
        // await Seed_Tables(_context,_context.DB_Warning_Comment,"Data/Seed/Warning_Comment/Warning_Comment.data.json");
    }


    public static async Task Seed_Tables<T>(DataContext _context, DbSet<T> Table, string FilePath) where T : class
    {
        if (await Table.AnyAsync()) return;
        var Data = await System.IO.File.ReadAllTextAsync(FilePath);
        var Data_Extended = JsonSerializer.Deserialize<List<T>>(Data);
        if (Data_Extended == null) return;
        foreach (var x in Data_Extended)
        {
            Table.Add(x);
        }
        await _context.SaveChangesAsync();
    }

    public static async Task SeedUsers(UserManager<AppUser> userManager, DataContext _context)
    {
        if (await userManager.Users.AnyAsync()) return;

        var userData = await System.IO.File.ReadAllTextAsync("Data/Seed/User/UserData.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
        if (users == null) return;
        var i = 0;
        foreach (var user in users)
        {
            i++;
            user.UserName = user.UserName.ToLower();
            await userManager.CreateAsync(user, "Pa$$w0rd");
            //if (user.UserName == "admin")
            //    await userManager.AddToRoleAsync(user, "Admin");
            //if (user.UserName != "admin")
            //    await userManager.AddToRoleAsync(user, "Member");
        }
    }
}