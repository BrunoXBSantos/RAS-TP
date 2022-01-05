using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class SportEmptyDto
{
    public string Description { get; set; }

    // Type (coletivo ou indivudual)
    public int sportType_Id { get; set; }

}