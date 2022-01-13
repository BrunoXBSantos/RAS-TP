using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;

public class UserDto
{
    //* App User Fields


    public int Id { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Name { get; set; }
    public string Contact { get; set; }
    [Required]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
    public Wallet wallet { get; set; }
}