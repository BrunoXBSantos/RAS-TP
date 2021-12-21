using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class MemberSimpleDto
{
    public int id { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public string UserName { get; set; }
    public string contact { get; set; }
    //IdentityUser Fields
    [Required]
    public string Email { get; set; }
}