using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Model.Football;

namespace API.RequestHandlers
{
    public interface IRequestHandler
    {
        //Method to get the releases of the repo provided by the url
         Task<ListEventAll> Get(string url);
    }
}