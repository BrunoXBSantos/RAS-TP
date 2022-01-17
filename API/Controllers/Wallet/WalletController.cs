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
    /// Get all money in euros.
    /// </summary>
    [HttpGet("/{idUser}/allMoney")]
    public async Task<ActionResult<AllMoneyDto>> GetAllMoneyEuros(int idUser)
    {
        
        var walletCoins = await _walletRepository.GetAllWalletCoinByUsers(idUser); 

        double sum = (double) walletCoins.Sum(e => (double) e.Balance * e.Coin.ConvertionToEuro);

        AllMoneyDto allMoneyDto = new AllMoneyDto();
        allMoneyDto.coinName = "Euro";
        allMoneyDto.balance = sum;

        return Ok(allMoneyDto);
    }

    /// <summary>
    /// Get list of coins by user id.
    /// </summary>
    [HttpGet("{idUser}")]
    public async Task<ActionResult<CoinDto>> GetCoinsByUser(int idUser)
    {
        
        var walletcoins = await _walletRepository.GetAllWalletCoinDisplayByUser(idUser); 

        if (walletcoins == null) return BadRequest("User has no balance for any currency");
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
            return BadRequest("Coin does not exist");
        
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
            return NotFound("Coin does not exist");
        
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


    /// <summary>
    /// Load wallet for a user Id .
    /// </summary>
    [HttpPut("{idUser}/changeCurrency")]
    public async Task<ActionResult<CoinDto>> changeCurrency(ChangeWalletCoinDto changeWalletCoinDto, int idUser)
    {
        if(!(await _walletRepository.CheckCoinExistById(changeWalletCoinDto.coinIDTo)))
            return NotFound("Coin does not exist");
        
        // vou buscar a walletCoin por onde irá ser retirado dinheiro
        WalletCoin walletCoinFrom = await _walletRepository.GetWalletCoinAsync(changeWalletCoinDto.appUserID, changeWalletCoinDto.coinIDFrom);
        
        // Nao tenho a moeda que quero trocar
        if(walletCoinFrom == null)
            return Unauthorized("Insufficient funds");

        // vou buscar a taxa de conversao para euros da moeda pretendida
        var coinTo = await _walletRepository.GetCoinByIdAsync(changeWalletCoinDto.coinIDTo);
        var coinFrom = await _walletRepository.GetCoinByIdAsync(changeWalletCoinDto.coinIDFrom);

        // vou converter em euros o que eu quero comprar
        //// vou ao valor pretendido e converto para euro
        // se a moeda de destino for euros, nao converto, senao converto para euros
        double coinTo2Euros = changeWalletCoinDto.BalanceBuy;
        if(coinTo.Id != 1)
            coinTo2Euros = changeWalletCoinDto.BalanceBuy * coinTo.ConvertionToEuro;

        // se a moeda de origem é euros, não converto, senao converto para euros
        var coinTo2Euros2CoinFrom = coinTo2Euros;
        if(coinFrom.Id != 1)
            coinTo2Euros2CoinFrom = (double) coinTo2Euros / coinFrom.ConvertionToEuro;
        // converto para a moeda que quero vender
         

        if(coinTo2Euros2CoinFrom > walletCoinFrom.Balance)
            return Unauthorized("Insufficient funds");
        
        
        // vou buscar a walletCoin por onde irá ser adicionado o valor comprado/trocado
        WalletCoin walletCoinTo = await _walletRepository.GetWalletCoinAsync(changeWalletCoinDto.appUserID, changeWalletCoinDto.coinIDTo);

        bool flagCreate = false;
        // verifico se a walletCoin existe
        if(walletCoinTo == null)
            flagCreate = true;
        
        // retiro o valor comprado/trocado
        walletCoinFrom.Balance -= (double) coinTo2Euros2CoinFrom;
        // adiciono o valor que pretendo comprar
        if(walletCoinTo == null){
            walletCoinTo = new WalletCoin();
            walletCoinTo.appUserID = changeWalletCoinDto.appUserID;
            walletCoinTo.coinID = changeWalletCoinDto.coinIDTo;
            walletCoinTo.Balance = changeWalletCoinDto.BalanceBuy;
        }
        else
            walletCoinTo.Balance += (double) changeWalletCoinDto.BalanceBuy;

        _walletRepository.Update(walletCoinFrom);
        if (!await _walletRepository.SaveAllAsync()){ 
            return BadRequest("Error");
        }

        if(flagCreate)
            _walletRepository.AddWalletCoin(walletCoinTo);
        else
            _walletRepository.Update(walletCoinTo);

        if (await _walletRepository.SaveAllAsync()){ 
            var walletcoins = await _walletRepository.GetAllWalletCoinDisplayByUser(idUser);
            return Ok(walletcoins);
        }
        return BadRequest("Error");
    }
    #endregion
    
}