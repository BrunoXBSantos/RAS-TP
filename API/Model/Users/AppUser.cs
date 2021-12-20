using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Model;

public class AppUser : IdentityUser<int>
{
    // IdentityUser Brings:
    //     username
    //     password
    //     password_salt
    //     email

    // Real name of the user
    public string Name { get; set; }

    // User Contact
    public string Contact { get; set; }

    // User Profile Image
    public byte[] ProfileImage { get; set; }

    // User Observations
    public string Observations { get; set; }

}
