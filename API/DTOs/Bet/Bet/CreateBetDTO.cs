using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class CreateBetDTO
{
    public float value { get; set; }
    public int type { get; set; }

    // Table AppUser
    public int appUserId { get; set; }

    // table Event
    public int _eventId { get; set; }
}