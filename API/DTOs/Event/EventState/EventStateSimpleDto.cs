using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class EventStateSimpleDto : EventStateEmptyDto
{
    public int Id { get; set; }
}