using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class CreateBetDTO
{
    public float value { get; set; }
    public int coinID { get; set; }
    
    [RegularExpression("1|X|2", ErrorMessage = "The type must be either '1', '2' or 'X' only.")]
    public string Result { get; set; }

    // Table AppUser
    public int appUserId { get; set; }

    // table Event
    public int _eventId { get; set; }
}