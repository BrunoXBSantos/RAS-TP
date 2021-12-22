using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class BetSimpleDto
{
    public UserDto appUser { get; set; }
    public BetStateEmptyDto betState { get; set; }
    public EventSimpleDto _event { get; set; }
    
}