using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class BetEmptyDto
{
    public int Id { get; set; }
    public float value { get; set; }

    // Table AppUser
    public int appUserId { get; set; }

    // table BetState
    public int betStateId { get; set; }

    // table Event
    public int _eventId { get; set; }


}