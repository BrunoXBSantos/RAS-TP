using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers;

public class PaginationParams
{

    
    private const int MaxPagesSize = 50;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPagesSize) ? MaxPagesSize : value;
    }
    public List<string> FilterOptions { get; set; }


    public List<FilterOptions> _FilterOptions;
    public List<FilterOptions> FilterOptions2
    {
        get { return _FilterOptions = filter(FilterOptions); }
        set { _FilterOptions = value; }
    }

    public List<FilterOptions> filter(List<string> FilterOptions)
    {
        if (FilterOptions != null)
        {
            _FilterOptions = new List<FilterOptions> { };
            foreach (var word in FilterOptions)
            {
                string[] words = word.Split('-');
                _FilterOptions.Add(new FilterOptions { field = words[0], operators = words[1], value = words[2] });
            }
            return _FilterOptions;
        }
        return null;
    }
    
}

public class FilterOptions
{
    public string field { get; set; }
    public string operators { get; set; }
    public string value { get; set; }
}
