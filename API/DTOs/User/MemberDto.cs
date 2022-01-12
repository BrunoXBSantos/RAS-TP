using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;

public class MemberDto
{
    public int id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string PhoneNumber { get; set; }
    public string email { get; set; }
    public bool ActiveNotification { get; set; }
    public string IpNotification { get; set; }
    public int PortNotification { get; set; }

}