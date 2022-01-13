using System.Diagnostics;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Model;
using API.Constants.WalletConstants;
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
            case 1: (wallet.eur += value);
                    break;
            case 2: (wallet.usd += value);
                    break;
            case 3: (wallet.gbp += value);
                    break;
            case 4: (wallet.cnh += value);
                    break;
            case 5: (wallet.jpy += value);
                    break;
            case 6: (wallet.ada += value);
                    break;
            case 7: (wallet.btc += value);
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
        bool i = true;
        if(!(await _walletRepository.checkWalletValue(idUser,type,value))){
            switch(type){
                case 1: if(wallet.usd >= value * E2U){wallet.usd -= value * E2U;}
                        else{
                            if(wallet.gbp >= value * E2G){wallet.gbp -= value * E2G;}
                            else{
                                if(wallet.cnh >= value * E2C){wallet.cnh -= value * E2C;}
                                else{
                                    if(wallet.jpy >= value * E2J){wallet.jpy -= value * E2J;}
                                    else{
                                        if(wallet.ada >= value * E2A){wallet.ada -= value * E2A;}
                                        else{
                                            if(wallet.btc >= value * E2B){wallet.btc -= value * E2B;}
                                            else{i = false;}
                                        }
                                    }
                                }
                            }
                        }
                        break;
                case 2: if(wallet.eur >= value * U2E){wallet.eur -= value * U2E;}
                        else{
                            if(wallet.gbp >= value * U2G){wallet.gbp -= value * U2G;}
                            else{
                                if(wallet.cnh >= value * U2C){wallet.cnh -= value * U2C;}
                                else{
                                    if(wallet.jpy >= value * U2J){wallet.jpy -= value * U2J;}
                                    else{
                                        if(wallet.ada >= value * U2A){wallet.ada -= value * U2A;}
                                        else{
                                            if(wallet.btc >= value * U2B){wallet.btc -= value * U2B;}
                                            else{i = false;}
                                        }
                                    }
                                }
                            }
                        }
                        break;
                case 3: if(wallet.usd >= value * G2U){wallet.usd -= value * G2U;}
                        else{
                            if(wallet.eur >= value * G2E){wallet.eur -= value * G2E;}
                            else{
                                if(wallet.cnh >= value * G2C){wallet.cnh -= value * G2C;}
                                else{
                                    if(wallet.jpy >= value * G2J){wallet.jpy -= value * G2J;}
                                    else{
                                        if(wallet.ada >= value * G2A){wallet.ada -= value * G2A;}
                                        else{
                                            if(wallet.btc >= value * G2B){wallet.btc -= value * G2B;}
                                            else{i = false;}
                                        }
                                    }
                                }
                            }
                        }
                        break;
                case 4: if(wallet.usd >= value * C2U){wallet.usd -= value * C2U;}
                        else{
                            if(wallet.gbp >= value * C2G){wallet.gbp -= value * C2G;}
                            else{
                                if(wallet.eur >= value * C2E){wallet.eur -= value * C2E;}
                                else{
                                    if(wallet.jpy >= value * C2J){wallet.jpy -= value * C2J;}
                                    else{
                                        if(wallet.ada >= value * C2A){wallet.ada -= value * C2A;}
                                        else{
                                            if(wallet.btc >= value * C2B){wallet.btc -= value * C2B;}
                                            else{i = false;}
                                        }
                                    }
                                }
                            }
                        }
                        break;
                case 5: if(wallet.usd >= value * J2U){wallet.usd -= value * J2U;}
                        else{
                            if(wallet.gbp >= value * J2G){wallet.gbp -= value * J2G;}
                            else{
                                if(wallet.cnh >= value * J2C){wallet.cnh -= value * J2C;}
                                else{
                                    if(wallet.eur >= value * J2E){wallet.eur -= value * J2E;}
                                    else{
                                        if(wallet.ada >= value * J2A){wallet.ada -= value * J2A;}
                                        else{
                                            if(wallet.btc >= value * J2B){wallet.btc -= value * J2B;}
                                            else{i = false;}
                                        }
                                    }
                                }
                            }
                        }
                        break;
                case 6: if(wallet.usd >= value * A2U){wallet.usd -= value * A2U;}
                        else{
                            if(wallet.gbp >= value * A2G){wallet.gbp -= value * A2G;}
                            else{
                                if(wallet.cnh >= value * A2C){wallet.cnh -= value * A2C;}
                                else{
                                    if(wallet.jpy >= value * A2J){wallet.jpy -= value * A2J;}
                                    else{
                                        if(wallet.eur >= value * A2E){wallet.eur -= value * A2E;}
                                        else{
                                            if(wallet.btc >= value * A2B){wallet.btc -= value * A2B;}
                                            else{i = false;}
                                        }
                                    }
                                }
                            }
                        }
                        break;
                case 7: if(wallet.usd >= value * B2U){wallet.usd -= value * B2U;}
                        else{
                            if(wallet.gbp >= value * B2G){wallet.gbp -= value * B2G;}
                            else{
                                if(wallet.cnh >= value * B2C){wallet.cnh -= value * B2C;}
                                else{
                                    if(wallet.jpy >= value * B2J){wallet.jpy -= value * B2J;}
                                    else{
                                        if(wallet.ada >= value * B2A){wallet.ada -= value * B2A;}
                                        else{
                                            if(wallet.eur >= value * B2E){wallet.eur -= value * B2E;}
                                            else{i = false;}
                                        }
                                    }
                                }
                            }
                        }
                        break;
            }
            if(!i){return BadRequest("Wallet without enough funds");}
            else{
                _walletRepository.UpdateWallet(wallet);
                return Ok(TakeMoneyWalletDTO);
            }
        }
        switch(type){
            case 1: (wallet.eur -= value);
                    break;
            case 2: (wallet.usd -= value);
                    break;
            case 3: (wallet.gbp -= value);
                    break;
            case 4: (wallet.cnh -= value);
                    break;
            case 5: (wallet.jpy -= value);
                    break;
            case 6: (wallet.ada -= value);
                    break;
            case 7: (wallet.btc -= value);
                    break;
        }

        _walletRepository.UpdateWallet(wallet);

        return Ok(TakeMoneyWalletDTO);
    }
    #endregion
}
