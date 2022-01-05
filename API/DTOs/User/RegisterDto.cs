using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
{
    [Required]
    public string Name { get; set; }
    public string Contact { get; set; }
    public string PhoneNumber { get; set; }

    //IdentityUser Fields
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string password { get; set; }
}
}