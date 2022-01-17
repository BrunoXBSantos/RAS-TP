using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class EventDisplayDto
{
    public int Id { get; set; }
    public string Team1 { get; set; }
    public string Team2 { get; set; }

    public double Home_Odd { get; set; }
    public double Tie_Odd { get; set; }
    public double Away_Odd { get; set; }

    // Table EventType
    public string type { get; set; }
    // Table EventState
    public string state { get; set; }

    // Table Sport
    public string sport { get; set; }
}