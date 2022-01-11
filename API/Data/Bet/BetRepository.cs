using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using API.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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

    #region CREATE
    #endregion

    #region READ
    #endregion

    #region UPDATE
    #endregion

    #region DELETE
    #endregion

    // add a bet to the DB
    public void AddBet(Bet bet)
    {
        _context.DB_Bet.Add(bet);
    }

    // get a Bet by id
    public async Task<Bet> GetBetByIdAsync(int id)
    {
        return await _context.DB_Bet
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
    }

    // get a BetEmptyDto by id
    public async Task<BetEmptyDto> GetBetEmptyByIdAsync(string id)
    {
        return await _context.DB_Bet
            .Where(x => x.Id == int.Parse(id))
            // .Include(s => s.Warnings)
            // .Include(s => s.Comments)
            .ProjectTo<BetEmptyDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    // get a BetSimpleDto by id
    public async Task<BetSimpleDto> GetBetSimpleByIdAsync(string id)
    {
        return await _context.DB_Bet
            .Where(x => x.Id == int.Parse(id))
            // .Include(s => s.Warnings)
            // .Include(s => s.Comments)
            .ProjectTo<BetSimpleDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    // get a list of BetSimpleDto by id User
    public async Task<PagedList<BetSimpleDto>> GetBetSimpleByIdUserAsync(BetParams betParams, string appUserId)
    {
        var query = _context.DB_Bet
                .Where(x => x.appUserId == int.Parse(appUserId))
                // .Include(s => s.Warnings)
                // .Include(s => s.Comments)
                .ProjectTo<BetSimpleDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .AsQueryable();

        return await PagedList<BetSimpleDto>.CreateAsync(query, 
                    betParams.PageNumber, betParams.PageSize,betParams.FilterOptions2);
    }

    // get Bets with state is open
    public async Task<List<BetEmptyDto>> GetBetsOpen()
    {
        var bets = await _context.DB_Bet
            .Where(x => x.betStateId == 1)
            .ProjectTo<BetEmptyDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return bets;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}