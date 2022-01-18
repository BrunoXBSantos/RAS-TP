using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// É o que é enviado para os observers
namespace API.Model;
public class Notification
{
    public string description { get; set; }
    public IServiceProvider provider { get; set; }

    public Notification(string description, IServiceProvider provider)
    {
        this.description = description;
        this.provider = provider;
    }
    
    
}