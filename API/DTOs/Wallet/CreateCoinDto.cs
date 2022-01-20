using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;

public class CreateCoinDto
{
    public string Name { get; set; }

    public double ConvertionToEuro { get; set; }
}