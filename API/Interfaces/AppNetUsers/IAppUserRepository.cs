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
    Task<UserDto> GetUserAsync(string username);
    Task<UserDto> GetByIdUserAsync(string id);
    Task<PagedList<UserDto>> GetUsersAsync(AppUserParams userParams);
    Task<List<MemberSimpleDto>> GetListUsersAsync();
    Task<IEnumerable<UserDto>> SearchUserAsync(MemberDto utilizador);        //Search
    Task<AppUser> GetUserByUsernameAsync(string UserName);
    void UpdateUser(AppUser appUser);

    public Task<bool> checkBalanceById(int id, int type, float value);
    public Task<bool> checkUserExistByIdAsync(string id);
    Task<bool> SaveAllAsync();
    
}