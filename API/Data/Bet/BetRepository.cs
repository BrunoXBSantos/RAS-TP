using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace API.Data;
public class BetRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BetRepository(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    // add a Bet to the DB
}