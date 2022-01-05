using API.Model;

namespace API.RequestHandlers
{
    public interface IRequestHandler
    {
        //Method to get the releases of the repo provided by the url
         Task<ListEventAll> Get(string url);
    }
}