using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class CoinDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public float ConvertionToEuro { get; set; }

}