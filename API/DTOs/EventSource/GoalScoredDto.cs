using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;

public class GoalScoredDto : MatchDto
{
    public string Goal { get; set; }
    #nullable enable
    public string? Played { get; set; }
}