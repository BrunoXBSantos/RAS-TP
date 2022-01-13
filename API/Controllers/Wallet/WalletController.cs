using System.Diagnostics;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class WalletController : BaseApiController
{
    private readonly ITokenService _tokenService;
    public readonly IAppUserRepository _appUserRepository;
    public readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public WalletController(IAppUserRepository appUserRepository, DataContext context, ITokenService tokenService, IMapper mapper, IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
        _appUserRepository = appUserRepository;
        _tokenService = tokenService;
        _context = context;
    }

    #region Create 
    [HttpPost]
    public async Task<ActionResult<WalletDTO>> CreateWallet(WalletDTO createWallet)
    {

        if(!(await _appUserRepository.checkUserExistByIdAsync(createWallet.id)))
            return NotFound("User Not Found");

        if(await _walletRepository.checkWalletByIdAsync(createWallet.id))
            return NotFound("Wallet already exists");

        var wallet = _mapper.Map<Wallet>(createWallet);

        _walletRepository.AddWallet(wallet);
        
        Console.WriteLine("Aqui metia a cena naquilo broooooo");

        if (await _walletRepository.SaveAllAsync()) 
            return Ok(_mapper.Map<WalletDto>(wallet));

        return BadRequest("Failed to create Wallet");

    }
    #endregion

    #region Read
    [HttpGet("User={idUser}")]
    public async Task<ActionResult<WalletDTO>> GetWallet(string idUser)
    {
        if(!(await _appUserRepository.checkUserExistByIdAsync(idUser)))
            return NotFound("User Not Found");
            
        var wallet = await _walletRepository.GetWalletByIdAsync(idUser);
        Response.AddPaginationHeader(wallet.CurrentPage, wallet.PageSize);

        return Ok(wallet);
    }
    #endregion

    #region Write
    [HttpPost]
    public async Task<ActionResult<AddMoneyWalletDTO>> AddMoneyWallet(int type,float value,string idUser)
    {
        if(!(await _appUserRepository.checkUserExistByIdAsync(idUser)))
            return NotFound("User Not Found");
        
        var wallet = await _walletRepository.GetWalletByIdAsync(idUser);
        switch(type){
            case 1: s = (wallet.eur += value);
                    break;
            case 2: s = (wallet.usd += value);
                    break;
            case 3: s = (wallet.gbp += value);
                    break;
            case 4: s = (wallet.cnh += value);
                    break;
            case 5: s = (wallet.jpy += value);
                    break;
            case 6: s = (wallet.ada += value);
                    break;
            case 7: s = (wallet.btc += value);
                    break;
        }

        _walletRepository.UpdateWallet(wallet);

        return Ok(AddMoneyWalletDTO);
    }

    public async Task<ActionResult<TakeMoneyWalletDTO>> TakeMoneyWallet(int type,float value,string idUser)
    {
        if(!(await _appUserRepository.checkUserExistByIdAsync(idUser)))
            return NotFound("User Not Found");
        
        var wallet = await _walletRepository.GetWalletByIdAsync(idUser);
        if(!(await _walletRepository.checkWalletValue(idUser,type,value)))
            return BadRequest("Wallet without enough funds");
        switch(type){
            case 1: s = (wallet.eur -= value);
                    break;
            case 2: s = (wallet.usd -= value);
                    break;
            case 3: s = (wallet.gbp -= value);
                    break;
            case 4: s = (wallet.cnh -= value);
                    break;
            case 5: s = (wallet.jpy -= value);
                    break;
            case 6: s = (wallet.ada -= value);
                    break;
            case 7: s = (wallet.btc -= value);
                    break;
        }

        _walletRepository.UpdateWallet(wallet);

        return Ok(TakeMoneyWalletDTO);
    }
    #endregion
}
