using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class AddMoneyWalletDTO
{
    
    [Required]
    public float val { get; set; }
    [Required]
    public float type { get; set; }
    [Required]
    public string userName { get; set; }

}