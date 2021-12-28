using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Model;

namespace API.Interfaces;
public interface IBetRepository
{
    public void AddBet(Bet bet);

    public Task<bool> SaveAllAsync();
    public Task<Bet> GetBetByIdAsync(int id);
    public Task<BetSimpleDto> GetBetSimpleByIdAsync(string id);
    public Task<BetEmptyDto> GetBetEmptyByIdAsync(string id);
    public Task<List<BetEmptyDto>> GetBetsOpen();
    public Task<PagedList<BetSimpleDto>> GetBetSimpleByIdUserAsync(BetParams betParams, string appUserId);
}