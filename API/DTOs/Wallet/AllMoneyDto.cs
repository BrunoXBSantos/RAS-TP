using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class AllMoneyDto
{
    public string coinName { get; set; }
    public double balance { get; set; }
}