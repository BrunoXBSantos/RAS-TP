

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

    public DbSet<Event> Events{ get; set; }

    #region Users
    public DbSet<AppUser> DB_AppUser { get; set; }
    #endregion

}