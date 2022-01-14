using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WalletController : BaseApiController
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;
    public WalletController(IMapper mapper,
                            IWalletRepository walletRepository 
                            
                         )
    {
        _mapper = mapper;
        _walletRepository = walletRepository;
    }

    #region READ
    /// <summary>
    /// Get list of coins.
    /// </summary>
    [HttpGet("ValueEnxanges")]
    public async Task<ActionResult<CoinDto>> GetCoins()
    {
        
        var coins = await _walletRepository.GetCoins(); 

        if (coins == null) return BadRequest("Coins not found");
        return Ok(coins);
    }

    /// <summary>
    /// Get list of coins by user id.
    /// </summary>
    [HttpGet("{idUser}")]
    public async Task<ActionResult<CoinDto>> GetCoinsByUser(int idUser)
    {
        
        var walletcoins = await _walletRepository.GetWalletCoinsByUser(idUser); 

        if (walletcoins == null) return BadRequest("User has no coins");
        return Ok(walletcoins);
    }
    #endregion  

    #region Update
    /// <summary>
    /// Load wallet for a user Id .
    /// </summary>
    [HttpPut("Load/{idUser}")]
    public async Task<ActionResult> LoadWallet(LoadWalletCoinDto loadWalletCoinDto, int idUser)
    {
        if(!(await _walletRepository.CheckCoinExistById(loadWalletCoinDto.coinID)))
            return BadRequest("Coin dont exist");
        
        // vou buscar a walletCoin
        WalletCoin walletCoin = await _walletRepository.GetWalletCoinAsync(loadWalletCoinDto.appUserID, loadWalletCoinDto.coinID);

        // se nao existir, crio
        if(walletCoin == null){
            WalletCoin walletCoin1 = new WalletCoin();
            _mapper.Map(loadWalletCoinDto, walletCoin1);
            _walletRepository.AddWalletCoin(walletCoin1);
            if (await _walletRepository.SaveAllAsync()){ 
                WalletCoinEmptyDto walletCoinEmptyDto = new WalletCoinEmptyDto();
                _mapper.Map(walletCoin1, walletCoinEmptyDto);
                    return Ok(walletCoinEmptyDto);
            }
            return BadRequest("Error load wallet");
        }
        else {
            // se existir atualizo
            walletCoin.Balance += loadWalletCoinDto.Balance;
            _walletRepository.Update(walletCoin);
            if (await _walletRepository.SaveAllAsync()){ 
                WalletCoinEmptyDto walletCoinEmptyDto = new WalletCoinEmptyDto();
                _mapper.Map(walletCoin, walletCoinEmptyDto);
                return Ok(walletCoinEmptyDto);
            }
            return BadRequest("Error load wallet");
        }
        
    }

    /// <summary>
    /// Load wallet for a user Id .
    /// </summary>
    [HttpPut("withdraw/{idUser}")]
    public async Task<ActionResult> withdrawWallet(LoadWalletCoinDto withdrawWalletCoinDto, int idUser)
    {
        if(!(await _walletRepository.CheckCoinExistById(withdrawWalletCoinDto.coinID)))
            return NotFound("Coin dont exist");
        
        // vou buscar a walletCoin
        WalletCoin walletCoin = await _walletRepository.GetWalletCoinAsync(withdrawWalletCoinDto.appUserID, withdrawWalletCoinDto.coinID);

        if(walletCoin == null){
            return NotFound("User does not have that currency.");
        }
        else {
            if(withdrawWalletCoinDto.Balance > walletCoin.Balance){
                return Unauthorized("Insufficient funds");
            }
            // se existir atualizo
            walletCoin.Balance -= withdrawWalletCoinDto.Balance;
            _walletRepository.Update(walletCoin);
            if (await _walletRepository.SaveAllAsync()){ 
                WalletCoinEmptyDto walletCoinEmptyDto = new WalletCoinEmptyDto();
                _mapper.Map(walletCoin, walletCoinEmptyDto);
                return Ok(walletCoinEmptyDto);
            }
            return BadRequest("Error withdraw coin in wallet");
        }
        
    }
    #endregion
    
}