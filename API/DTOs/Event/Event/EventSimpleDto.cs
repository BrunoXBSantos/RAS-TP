using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class EventSimpleDto : EventEmptyDto
{
    // Table EventType
    public EventTypeEmptyDto eventType { get; set; }

    // Table Sport
    public SportEmptyDto sport { get; set; }
    // Table EventState
    public EventStateEmptyDto eventState { get; set; }
    // Table Bet
    public ICollection<BetEmptyDto> bets { get; set; }

}