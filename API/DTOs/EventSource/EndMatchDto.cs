using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class EndMatchDto : MatchDto
{
    public int Team1Goals { get; set; }
    public int Team2Goals { get; set; }
}