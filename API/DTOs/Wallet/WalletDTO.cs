using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class WalletDTO
{
    [Required]
    public int id { get; set; }
    [Required]
    public float eur { get; set; }
    [Required]
    public float usd { get; set; }
    [Required]
    public float gbp { get; set; }
    [Required]
    public float cnh { get; set; }
    [Required]
    public float jpy { get; set; }
    [Required]
    public float ada { get; set; }
    [Required]
    public float btc { get; set; }

}