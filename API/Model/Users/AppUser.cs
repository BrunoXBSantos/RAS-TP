using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Model;

public class AppUser : IdentityUser<int>
{
    // IdentityUser Brings:
    //     username
    //     password
    //     password_salt
    //     email

    // Real name of the user
    public string Name { get; set; }

    // User Contact
    public string Contact { get; set; }
    public double Balance { get; set; }

    // Ip e Port são para receber as notificaçoes
    public bool ActiveNotification { get; set; }
    public string IpNotification { get; set; }
    public int PortNotification { get; set; }

    public ICollection<Bet> bets {get; set;}
    public ICollection<AppUserRole> UserRoles { get; set; }

    public ICollection<WalletCoin> WalletCoin { get; set; }

}
