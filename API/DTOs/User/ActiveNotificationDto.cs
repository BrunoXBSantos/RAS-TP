using System.ComponentModel.DataAnnotations;

namespace API.DTOs;
public class ActiveNotificationDto
{
    [Required]
    public string UserName { get; set; }
    public bool ActiveNotification { get; set; }

    public string IpNotification { get; set; }

    public int PortNotification { get; set; }
}