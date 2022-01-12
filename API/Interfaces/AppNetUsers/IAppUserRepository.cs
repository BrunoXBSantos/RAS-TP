using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Model;

namespace API.Interfaces;
public interface IAppUserRepository
{
    //C(reate) R(ead) U(pdate) D(elete)
    Task<MemberDto> GetUserAsync(string username);
    public Task<UserDto> GetUserDtoAsync(string username);
    Task<MemberDto> GetByIdUserAsync(string id);
    Task<PagedList<MemberDto>> GetUsersAsync(AppUserParams userParams);
    //Task<List<MemberSimpleDto>> GetListUsersAsync();
    Task<IEnumerable<UserDto>> SearchUserAsync(MemberDto utilizador);        //Search
    Task<AppUser> GetUserByUsernameAsync(string UserName);
    public Task<ActiveNotificationSimpleDto> GetActiveNotificationSimpleDtoIdAsync(int id);
    void UpdateUser(AppUser appUser);

    public Task<bool> checkBalanceById(int id, float balanceBet);
    public Task<bool> checkUserExistByIdAsync(string id);
    public Task<bool> checkIpPortNotificationByUsernameAsync(string username);
    Task<bool> SaveAllAsync();
    
}