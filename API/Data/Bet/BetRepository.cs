using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Model;
using AutoMapper;

namespace API.Data;
public class BetRepository : IBetRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BetRepository(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    // add a bet to the DB
    public void AddBet(Bet bet)
    {
        _context.DB_Bet.Add(bet);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}