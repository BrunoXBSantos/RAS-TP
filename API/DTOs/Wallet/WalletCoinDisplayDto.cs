using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class WalletCoinDisplayDto
{
    public double Balance { get; set; }
    public string CoinName { get; set; }
}