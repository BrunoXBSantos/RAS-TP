using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;

public class LoadWalletCoinDto
{
    public float Balance { get; set; }
    public int appUserID { get; set; }
    public int coinID { get; set; }
}