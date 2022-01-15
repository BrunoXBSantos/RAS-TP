using API.DTOs;
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


    #region CREATE
    // add a walletCoin to the DB
    public void AddWalletCoin(WalletCoin walletCoin)
    {
        _context.DB_WalletCoin.Add(walletCoin);
    }
    #endregion

    #region READ
    // Get all coins in the system 
    public async Task<List<CoinDto>> GetCoins()
    {
        return await _context.DB_Coin
            .ProjectTo<CoinDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    // Get all walletCoins from user 
    public async Task<List<WalletCoinDisplayDto>> GetWalletCoinsByUser(int idUser)
    {
        return await _context.DB_WalletCoin
            .Where(wc => wc.appUserID == idUser)
            .ProjectTo<WalletCoinDisplayDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    // Get walletCoin by id 
    public async Task<WalletCoin> GetWalletCoinAsync(int appUserId, int coinId)
    {
        return await _context.DB_WalletCoin
            .Where(x => x.appUserID == appUserId && x.coinID == coinId)
            .SingleOrDefaultAsync();
    }

     public async Task<WalletCoin> GetWalletCoinDetailedAsync(int appUserId, int coinId)
    {
        return await _context.DB_WalletCoin
            .Where(x => x.appUserID == appUserId && x.coinID == coinId)
            .Include(c => c.Coin)
            .SingleOrDefaultAsync();
    }

    
    public async Task<Coin> GetCoinByIdAsync(int coinId)
    {
        return await _context.DB_Coin
            .Where(x => x.Id == coinId)
            .SingleOrDefaultAsync();
    }

    #endregion

    #region UPDATE
    // Update a wallet_coin on the DB
    public void Update(WalletCoin walletCoin)
    {
        _context.Entry(walletCoin).State = EntityState.Modified;
    }
    #endregion

    #region DELETE
    #endregion

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    #region FUNCTIONS
    // Check Coin Exist By Id
    public async Task<bool> CheckCoinExistById(int id)
    {
        return await _context.DB_Coin
                .AnyAsync(c => c.Id == id);
    }

    // Check Coin Exist By Id
    public async Task<bool> CheckBalance(int appUserId, int coinId, float balance)
    {
        WalletCoin walletCoin =  await _context.DB_WalletCoin
                .SingleAsync(wc => wc.appUserID == appUserId && wc.coinID == coinId);
        return balance >= walletCoin.Balance;
    }

    
    #endregion
}