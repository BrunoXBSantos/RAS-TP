using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class WalletCoinDTO
{
    public int Id { get; set; }

    public float Balance { get; set; }

    // Table AppUser
    public int AppUserID { get; set; }

    // Table Coin
    public int CoinID { get; set; }

}