using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class ChargeBalanceDTO
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public double Balance { get; set; }

}