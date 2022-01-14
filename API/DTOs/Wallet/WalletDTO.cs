using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class WalletDTO
{
    [Required]
    public int CoinID { get; set; }
    [Required]
    public float Montante { get; set; }
    [Required]
    public int appUserID { get; set; }
    
}