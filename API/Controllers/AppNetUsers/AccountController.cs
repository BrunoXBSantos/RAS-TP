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



    public AccountController(IAppUserRepository appUserRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _tokenService = tokenService;
        _appUserRepository = appUserRepository;
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

        var result = await _userManager.CreateAsync(Userx, registerDto.password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        //var roleResult = await _userManager.AddToRoleAsync(Userx, "Member");

        //if (!roleResult.Succeeded) BadRequest(result.Errors);

        return Ok(_mapper.Map<RegisterDto>(Userx));
    }
    

    [HttpPost, Route("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        if (user == null)
            return BadRequest("Inicio de sessão sem sucesso");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.password, false);

        if (!result.Succeeded) return Unauthorized();

        var userDtoResult = await _appUserRepository.GetUserAsync(user.UserName);
        //userDtoResult.Token = await _tokenService.CreateToken(user);

        return userDtoResult;
    }


    [NonAction]
    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }

}

