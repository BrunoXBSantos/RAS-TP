using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using API.Model;

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Bet
        // State
        builder.Entity<Bet>()
            .HasOne(b => b.betState)
            .WithMany(s => s.bets)
            .HasForeignKey(b => b.betStateId)
            .OnDelete(DeleteBehavior.NoAction);

        // AppUser
        builder.Entity<Bet>()
            .HasOne(b => b.appUser)
            .WithMany(u => u.bets)
            .HasForeignKey(b => b.appUserId)
            .OnDelete(DeleteBehavior.ClientCascade);

        // Event
        builder.Entity<Bet>()
            .HasOne(b => b._event)
            .WithMany(e => e.bets)
            .HasForeignKey(b => b._eventId)
            .OnDelete(DeleteBehavior.NoAction);
        #endregion

        #region Event
        //EventType
        builder.Entity<EventDB>()
            .HasOne(e => e.eventType)
            .WithMany(et => et.events)
            .HasForeignKey(fk => fk.eventTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Entity<EventDB>()
            .HasOne(e => e.sport)
            .WithMany(s => s.events)
            .HasForeignKey(fk => fk.sportId)
            .OnDelete(DeleteBehavior.NoAction);
        #endregion

        #region Sport
        builder.Entity<Sport>()
            .HasOne(s => s.sportType)
            .WithMany(t => t.sports)
            .HasForeignKey(fk => fk.sportType_Id)
            .OnDelete(DeleteBehavior.NoAction);
        #endregion

    }

    #region EventDB
    public DbSet<EventDB> DB_Events{ get; set; }
    public DbSet<EventType> DB_EventType{ get; set; }
    public DbSet<Sport> DB_Sports{ get; set; }
    #endregion

    #region Users
    public DbSet<AppUser> DB_AppUser { get; set; }
    #endregion

    #region Bet
    public DbSet<Bet> DB_Bet { get; set; }
    public DbSet<BetState> DB_BetState { get; set; }
    #endregion

    // #region EventDB
    // #endregion

}