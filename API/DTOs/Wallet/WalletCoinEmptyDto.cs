using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class WalletCoinEmptyDto
{
    public int Id { get; set; }
    public double Balance { get; set; }
    public int appUserID { get; set; }
    public int coinID { get; set; }
}