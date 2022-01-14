
namespace API.Model;

public class WalletCoin{

    public int Id { get; set; }
    public float Balance { get; set; }
    public int appUserID { get; set; }
    public AppUser appUser { get; set; }

    public int coinID { get; set; }
    public Coin Coin { get; set; }


}