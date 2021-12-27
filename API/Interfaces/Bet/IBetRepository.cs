using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;

namespace API.Interfaces;
public interface IBetRepository
{
    public void AddBet(Bet bet);

    public Task<bool> SaveAllAsync();
}