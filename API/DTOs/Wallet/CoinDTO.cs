using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class CoinDTO
{
    [Required]
    public int CoinID { get; set; }
    [Required]
    public float Name { get; set; }
    [Required]
    public int appUserID { get; set; }
    
}