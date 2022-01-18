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

    // add a bet to the DB
    public void AddBet(Bet bet)
    {
        _context.DB_Bet.Add(bet);
    }
    #endregion

    #region READ

    // get a Bet by id
    public async Task<Bet> GetBetByIdAsync(int id)
    {
        return await _context.DB_Bet
            .Where(x => x.Id == id)
            // .Include(s => s.Warnings)
            // .Include(s => s.Comments)
            .SingleOrDefaultAsync();
    }

    // get a BetEmptyDto by id
    public async Task<BetEmptyDto> GetBetEmptyByIdAsync(int id)
    {
        return await _context.DB_Bet
            .Where(x => x.Id == id)
            // .Include(s => s.Warnings)
            // .Include(s => s.Comments)
            .ProjectTo<BetEmptyDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    // get a BetSimpleDto by id
    public async Task<BetSimpleDto> GetBetSimpleByIdAsync(int id)
    {
        return await _context.DB_Bet
            .Where(x => x.Id == id)
            // .Include(s => s.Warnings)
            // .Include(s => s.Comments)
            .ProjectTo<BetSimpleDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    // get a list of BetSimpleDto by id User
    public async Task<PagedList<BetEmptyDto>> GetBetsSimpleByIdAsync(BetParams paginationParams, int appUserId)
    {
        var query = _context.DB_Bet
                .Where(x => x.appUserId == appUserId)
                // .Include(s => s.Warnings)
                // .Include(s => s.Comments)
                .ProjectTo<BetEmptyDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .AsQueryable();

        return await PagedList<BetEmptyDto>.CreateAsync(query, 
                paginationParams.PageNumber, paginationParams.PageSize,paginationParams.FilterOptions2);
    }

    // get Bets with state is == state
    public async Task<PagedList<BetEmptyDto>> GetBetsWithState(PaginationParams paginationParams, int state)
    {
        var query = _context.DB_Bet
            .Where(x => x.betStateId == state)
            .ProjectTo<BetEmptyDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .AsQueryable();
        
        return await PagedList<BetEmptyDto>.CreateAsync(query, 
                paginationParams.PageNumber, paginationParams.PageSize,paginationParams.FilterOptions2);

    }

    // get Bets with state is open or started to backgroud
    public async Task<List<BetEmptyDto>> GetBetsOpen()
    {
        var bets = await _context.DB_Bet
            .Where(x => x.betStateId == 1 || x.betStateId == 4)
            .ProjectTo<BetEmptyDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return bets;
    }

    #endregion

    #region UPDATE
    public void UpdateBet(Bet bet)
    {
        _context.Entry(bet).State = EntityState.Modified;
    }
    #endregion

    #region DELETE
    #endregion

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}