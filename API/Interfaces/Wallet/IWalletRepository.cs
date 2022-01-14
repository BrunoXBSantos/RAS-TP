using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Model;

namespace API.Interfaces;

public interface IWalletRepository
{


    #region CREATE
    void AddWalletCoin(WalletCoin walletCoin);
    #endregion

    #region READ
    Task<List<CoinDto>> GetCoins();
    Task<WalletCoin> GetWalletCoinAsync(int appUserId, int coinId);
    Task<List<WalletCoinDisplayDto>> GetWalletCoinsByUser(int idUser);

    #endregion

    #region UPDATE
    void Update(WalletCoin walletCoin);
    #endregion

    #region DELETE

    #endregion
    Task<bool> SaveAllAsync();

    #region FUNCTIONS
    Task<bool> CheckCoinExistById(int id);
    Task<bool> CheckBalance(int appUserId, int coinId, float balance);

    #endregion
}