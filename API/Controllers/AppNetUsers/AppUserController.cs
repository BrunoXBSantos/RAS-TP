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

//[Authorize]
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

    #region CREATE
    #endregion

    #region READ
    /// <summary>
    /// Get All Users.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<MemberDto>> GetUsers([FromQuery] AppUserParams userParams)
    {
        var users = await _appUserRepository.GetUsersAsync(userParams);
        Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
        return Ok(users);
    }

    /// <summary>
    /// Get User by Id or Username.
    /// </summary>
    [HttpGet("{idOrUsername}")]
    public async Task<ActionResult<MemberDto>> GetUser(string idOrUsername)
    {
        var user = new MemberDto();
        if (IsDigitsOnly(idOrUsername))
        {
            user = await _appUserRepository.GetByIdUserAsync(idOrUsername);
        }
        else { user = await _appUserRepository.GetUserAsync(idOrUsername); }
        if (user == null) return BadRequest("User not found");
        return Ok(user);
    }

    // [HttpGet, Route("ListUsers")]
    // public async Task<ActionResult<MemberDto>> GetListUsers()
    // {
    //     var equipment = await _appUserRepository.GetListUsersAsync();
    //     return Ok(equipment);
    // }
    #endregion

    #region UPDATE

    /// <summary>
    /// Update a user by username.
    /// </summary>
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

    /// <summary>
    /// Charge Balance by username.
    /// </summary>
    [HttpPut("{Username}/ChargeBalance")]
    public async Task<ActionResult> ChargeBalance(ChargeBalanceDTO chargeBalanceDTO)
    {
        AppUser user = await _appUserRepository.GetUserByUsernameAsync(chargeBalanceDTO.UserName);
        if (user != null){
            if(chargeBalanceDTO.Balance < 0 ){
                // verifica se tem saldo suficiente para retirar
                if(await _appUserRepository.checkBalanceById(user.Id, chargeBalanceDTO.Balance))
                    return Unauthorized("Insufficient funds");

            }
            user.Balance += chargeBalanceDTO.Balance;
            _appUserRepository.UpdateUser(user);
            if (await _appUserRepository.SaveAllAsync())
            {
                _mapper.Map(user, chargeBalanceDTO);
                return Ok(chargeBalanceDTO);
            }
        }
        return BadRequest("User " + chargeBalanceDTO.UserName + " not updated");
    }

    /// <summary>
    /// Change Password by username.
    /// </summary>
    [HttpPut("changePassword")]
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

    /// <summary>
    /// Active Notification by username.
    /// </summary>
    [HttpPut("{Username}/ActiveNotification")]
    public async Task<ActionResult> ActiveNotification(ActiveNotificationDto activeNotificationDto)
    {
        if(activeNotificationDto.ActiveNotification){
            // se nao tiver na BD a port e o IP
            if (!await _appUserRepository.checkIpPortNotificationByUsernameAsync(activeNotificationDto.UserName)){
                // e se nao for definido no corpo
                if(activeNotificationDto.IpNotification == null || activeNotificationDto.PortNotification == 0)
                    return BadRequest("Need to set IP and Port");
            }
        }


        AppUser user = await _appUserRepository.GetUserByUsernameAsync(activeNotificationDto.UserName);
        _mapper.Map(activeNotificationDto, user);

        _appUserRepository.UpdateUser(user);
        if (await _appUserRepository.SaveAllAsync())
        {
            _mapper.Map(user, activeNotificationDto);
            return Ok(activeNotificationDto);
        }
        return BadRequest("Error to update active Notification");
    }
    #endregion

    #region DELETE

    /// <summary>
    /// Delete a username.
    /// </summary>
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
    #endregion


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