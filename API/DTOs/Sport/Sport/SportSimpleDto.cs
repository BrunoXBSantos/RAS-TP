using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class SportSimpleDto : SportEmptyDto
{
    public int Id { get; set; }
    public SportTypeEmptyDto SportType { get; set; }
}
