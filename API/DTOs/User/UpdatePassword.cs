using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;

public class UpdatePassword
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public string confirmPassword { get; set; }
}
