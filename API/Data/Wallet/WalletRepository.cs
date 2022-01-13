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
public class WalletRepository : IWalletRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public WalletRepository(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    // add a wallet to the DB
    public void AddWallet(Wallet wallet)
    {
        _context.DB_Wallet.Add(wallet);
    }

    // get a wallet by id User
    public async Task<Wallet> GetWalletByIdAsync(string appUserId)
    {
        return await _context.DB_Wallet
            .Where(x => x.Id == int.Parse(appUserId))
            .SingleOrDefaultAsync();
    }

    public async Task<bool> checkWalletByIdAsync(string appUserId)
    {
        return await _context.DB_Wallet
            .AnyAync(x => x.Id == int.Parse(appUserId));
    }

    public async Task<List<WalletDTO>> GetListWalletsAsync()
    {
        return await _context.DB_Wallet
                             .ProjectTo<WalletDTO>(_mapper.ConfigurationProvider)
                             .ToListAsync();
    }

    public async Task<bool> checkWalletValue(string id, int type, float value){
        var wallet = await _context.DB_Wallet
                .Where(x => x.Id == int.Parse(id))
                .SingleOrDefaultAsync();
        bool s;
        switch(type){
            case 1: s = (wallet.eur >= value);
                    break;
            case 2: s = (wallet.usd >= value);
                    break;
            case 3: s = (wallet.gbp >= value);
                    break;
            case 4: s = (wallet.cnh >= value);
                    break;
            case 5: s = (wallet.jpy >= value);
                    break;
            case 6: s = (wallet.ada >= value);
                    break;
            case 7: s = (wallet.btc >= value);
                    break;
        }
        return s;
    }

    public void UpdateWallet(Wallet wallet)
    {
        _context.Entry(wallet).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}