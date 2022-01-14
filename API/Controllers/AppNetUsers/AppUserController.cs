using API.Data;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AppUserController : BaseApiController
{
    private readonly ITokenService _tokenService;
    public readonly IAppUserRepository _appUserRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly DataContext _context;


    public AppUserController(IAppUserRepository appUserRepository, DataContext context, ITokenService tokenService, IMapper mapper, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _appUserRepository = appUserRepository;
        _tokenService = tokenService;
        _context = context;
    }
    //C(reate) R(ead) U(pdate) D(elete)

    //Read
    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetUsers([FromQuery] AppUserParams userParams)
    {
        var users = await _appUserRepository.GetUsersAsync(userParams);
        Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
        return Ok(users);
    }
    //[Authorize]
    [HttpGet("{username}")]
    public async Task<ActionResult<UserDto>> GetUser(string username)
    {
        var user = new UserDto();
        if (IsDigitsOnly(username))
        {
            user = await _appUserRepository.GetByIdUserAsync(username);
        }
        else { user = await _appUserRepository.GetUserAsync(username); }
        if (user == null) return BadRequest("User not found");
        return Ok(user);
    }
    //[Authorize]
    [HttpGet, Route("ListUsers")]
    public async Task<ActionResult<MemberSimpleDto>> GetListUsers()
    {
        var equipment = await _appUserRepository.GetListUsersAsync();
        return Ok(equipment);
    }

    //Update

    //[Authorize]
    [HttpPut, DisableRequestSizeLimit]
    public async Task<ActionResult> EditUser(UserUpdateDto userUpdateDto)
    {
        var user = await _appUserRepository.GetUserByUsernameAsync(userUpdateDto.UserName);
        if (user != null)
        {
            _mapper.Map(userUpdateDto, user);
            _appUserRepository.UpdateUser(user);
            if (await _appUserRepository.SaveAllAsync())
            {
                _mapper.Map(user, userUpdateDto);
                return Ok(userUpdateDto);
            }
        }
        return BadRequest("User " + userUpdateDto?.UserName + " not updated");
    }

    //[Authorize]
    [HttpPut("ChargeBalance/{Username}")]
    public async Task<ActionResult> ChargeBalance(WalletDTO chargeBalanceDTO)
    {
        AppUser user = await _appUserRepository.GetUserByUsernameAsync(chargeBalanceDTO.Id);
        if (user != null)
        {

            user.wallet.eur += walletDTO.eur;
            user.wallet.usd += walletDTO.usd;
            user.wallet.gbp += walletDTO.gbp;
            user.wallet.cnh += walletDTO.cnh;
            user.wallet.jpy += walletDTO.jpy;
            user.wallet.ada += walletDTO.ada;
            user.wallet.btc += walletDTO.btc;
            _appUserRepository.UpdateUser(user);
            if (await _appUserRepository.SaveAllAsync())
            {
                _mapper.Map(user, chargeBalanceDTO);
                return Ok(chargeBalanceDTO);
            }
        }
        return BadRequest("User " + chargeBalanceDTO.Id + " not updated");
    }
    //[Authorize]
    [HttpPut("switchPW/{username}")]
    public async Task<ActionResult<LoginDto>> UpdateUserPassword(UpdatePassword updatePassword)
    {
        var user = await _appUserRepository.GetUserByUsernameAsync(updatePassword?.UserName);

        if (user != null)
        {
            var result = await _userManager.ChangePasswordAsync(user, updatePassword.password, updatePassword.confirmPassword);
            if (result.Succeeded)
            {
                return Ok(user);
            }
        }
        return BadRequest("Password of " + updatePassword?.UserName + " not updated");

    }

    //Delete
    //[Authorize]
    [HttpDelete("{username}")]
    public async Task<ActionResult> DeleteUser(string username)
    {
        var user = await _appUserRepository.GetUserByUsernameAsync(username);

        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(user);
            }
        }
        return BadRequest(username + " not Deleted");

    }

    //Functions

    [NonAction]
    bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }
}