using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using API.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.AppNetUsers;

public class AppUserRepository : IAppUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    public AppUserRepository(DataContext context, IMapper mapper, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _context = context;
    }

    //C(reate) R(ead) U(pdate) D(elete)


    #region CREATE
    #endregion

    #region READ
    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _userManager.Users
                                 .Where(x => x.Id == id)
                                 .SingleOrDefaultAsync();
    }
    public async Task<MemberDto> GetUserAsync(string username)
    {
        return await _userManager.Users
                                 .Where(x => x.UserName == username)
                                 .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync();
    }
    
    // Obtem o userDto, para o login
    public async Task<UserDto> GetUserDtoAsync(string username)
    {
        return await _userManager.Users
                                 .Where(x => x.UserName == username)
                                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync();
    }

    public async Task<PagedList<MemberDto>> GetUsersAsync(AppUserParams userParams)
    {
        var query = _userManager.Users
                        .OrderBy(u => u.Id)
                        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                        .AsNoTracking();

        return await PagedList<MemberDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize, userParams.FilterOptions2);
    }

     public async Task<MemberDto> GetByIdUserAsync(string id)
    {
        return await _userManager.Users
                                 .Where(x => x.Id == int.Parse(id))
                                 .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync();
    }

    public async Task<AppUser> GetUserByUsernameAsync(string UserName)
    {
        return await _context.DB_AppUser.SingleOrDefaultAsync(x => x.UserName == UserName);
    }

    // obtem os dados da notificaçao. Se estao ativas e a port e o Ip
    public async Task<ActiveNotificationSimpleDto> GetActiveNotificationSimpleDtoIdAsync(int id)
    {
        return await _userManager.Users
                                 .Where(x => x.Id == id)
                                 .ProjectTo<ActiveNotificationSimpleDto>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync();
    }
    #endregion

    #region UPDATE
    public void UpdateUser(AppUser appUser)
    {
        _context.Entry(appUser).State = EntityState.Modified;
    }
    #endregion

    #region DELETE
    #endregion
     //Read
    
    //Functions
    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    
    // Verifica se o saldo permite efetuar uma aposta de "balanceBet"
    public async Task<bool> checkBalanceById(int id, double balanceBet){
        var user = await _context.DB_AppUser
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
        return (user.Balance >= balanceBet);

    }

    public async Task<bool> checkUserExistByIdAsync(string id){
        return await _context.DB_AppUser
            .AnyAsync(user => user.Id == int.Parse(id));
    }

    // ver se as notificaçoes estao ativas
    public async Task<bool> checkIpPortNotificationByUsernameAsync(string username){
        var user = await _context.DB_AppUser
            .SingleOrDefaultAsync(user => user.UserName == username);
        if(user.IpNotification == null || user.PortNotification == 0)
            return false;
        return true;
    }
   

    

    public async Task<List<MemberDto>> GetListUsersAsync()
    {
        return await _context.DB_AppUser
                             .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                             .ToListAsync();
    }

    public async Task<IEnumerable<UserDto>> SearchUserAsync(MemberDto user)
    {
        return await _userManager.Users
                   .Where(x => x.UserName.Contains(user.username))
                   .Where(x => x.Name.Contains(user.name))
                   .Where(x => x.PhoneNumber.Contains(user.PhoneNumber))
                   .Where(x => x.Email.Contains(user.email))
                   .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                   .ToListAsync();
    }

    

    

    //Update
    


    



}