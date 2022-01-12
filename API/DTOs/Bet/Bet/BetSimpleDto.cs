using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class BetSimpleDto : BetEmptyDto
{
    public MemberDto appUser { get; set; }
    public BetStateEmptyDto betState { get; set; }
    public EventEmptyDto _event { get; set; }
    
}