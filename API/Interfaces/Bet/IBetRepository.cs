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
    public Task<Bet> GetBetByIdAsync(int id);
    public Task<bool> SaveAllAsync();
    public Task<BetSimpleDto> GetBetSimpleByIdAsync(int id);
    public Task<BetEmptyDto> GetBetEmptyByIdAsync(int id);
    int GetNumberBets();
    public Task<PagedList<BetEmptyDto>> GetBetsWithState(PaginationParams paginationParams, int state);
    public Task<PagedList<BetEmptyDto>> GetBetsSimpleByIdAsync(BetParams betParams, int appUserId);
    public void UpdateBet(Bet bet);

    public Task<List<BetEmptyDto>> GetBetsOpen();

}