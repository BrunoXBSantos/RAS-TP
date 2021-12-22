using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;

public class UserUpdateDto
{
    public string name { get; set; }
    public string contact { get; set; }
    //IdentityUser Fields
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
