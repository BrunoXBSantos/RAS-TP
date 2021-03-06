using API.DTOs;
using API.Interfaces;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public readonly IAppUserRepository _appUserRepository;
    public readonly IWalletRepository _walletRepository;



    public AccountController(IAppUserRepository appUserRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper, IWalletRepository walletRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _tokenService = tokenService;
        _appUserRepository = appUserRepository;
        _walletRepository = walletRepository;
    }


    // register a user
    [HttpPost, Route("register"), DisableRequestSizeLimit]
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.UserName))
            return BadRequest("The User already exists in the system");
        var Userx = new AppUser();
        _mapper.Map(registerDto, Userx);
        Userx.UserName = registerDto.UserName.ToLower();
        Userx.Balance = 0;

        var result = await _userManager.CreateAsync(Userx, registerDto.password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        var roleResult = await _userManager.AddToRoleAsync(Userx, "Member");

        if (!roleResult.Succeeded) BadRequest(result.Errors);
        
        return Ok(_mapper.Map<RegisterDto>(Userx));

        // var appUser = await _appUserRepository.GetUserByUsernameAsync(registerDto.UserName);

        // // 
        // WalletCoin walletCoin = new WalletCoin();
        // walletCoin.appUserID = appUser.Id;
        // walletCoin.coinID = 1;
        // walletCoin.Balance = 0;

        // _walletRepository.AddWalletCoin(walletCoin);
        // if(await _walletRepository.SaveAllAsync())
        //     return Ok(_mapper.Map<RegisterDto>(Userx));
        // return BadRequest("Error to register the User");
    }
    

    [HttpPost, Route("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        if (user == null)
            return BadRequest("Inicio de sess??o sem sucesso");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.password, false);

        if (!result.Succeeded) return Unauthorized();

        var userDtoResult = await _appUserRepository.GetUserDtoAsync(user.UserName);
        userDtoResult.Token = await _tokenService.CreateToken(user);

        return userDtoResult;
    }


    [NonAction]
    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }

}

