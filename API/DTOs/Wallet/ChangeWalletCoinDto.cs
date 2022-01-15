using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;

public class ChangeWalletCoinDto
{
    public int appUserID { get; set; }
    public float BalanceBuy { get; set; }
    public int coinIDFrom { get; set; }
    public int coinIDTo { get; set; }
}