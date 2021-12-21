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
    //Create

    //Read
    Task<UserDto> GetUserAsync(string username);
    Task<UserDto> GetByIdUserAsync(string id);

    Task<PagedList<UserDto>> GetUsersAsync(AppUserParams userParams);
    Task<List<MemberSimpleDto>> GetListUsersAsync();
    Task<IEnumerable<UserDto>> SearchUserAsync(MemberDto utilizador);        //Search
    Task<AppUser> GetUserByUsernameAsync(string UserName);
    //Task<AppUser> GetUtilizadorByIdAsync(int id);

    //         Task<Utilizador> GetUtilizadorByUsernameAsync(string username);
    //         Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
    //         Task<MemberDto> GetMemberAsync(string username);
    //Update
    void UpdateUser(AppUser appUser);
    //     //Delete
    //         void Delete(Utilizador utilizador);

    //Functions
    Task<bool> SaveAllAsync();
    
}