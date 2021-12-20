

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using API.Model;
using API.Entities.Bet;

namespace API.Data;
public class DataContext : IdentityDbContext<
                                AppUser,
                                IdentityRole<int>, int,
                                IdentityUserClaim<int>,
                                IdentityUserRole<int>,
                                IdentityUserLogin<int>,
                                IdentityRoleClaim<int>,
                                IdentityUserToken<int>>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    #region EventDB
    public DbSet<EventDB> Events{ get; set; }
    public DbSet<EventType> EventType{ get; set; }
    public DbSet<Sport> Sports{ get; set; }
    #endregion

    #region Users
    public DbSet<AppUser> AppUser { get; set; }
    #endregion

    #region Bet
    public DbSet<Bet> Bet { get; set; }
    public DbSet<BetState> BetState { get; set; }
    #endregion

    // #region EventDB
    // #endregion

}