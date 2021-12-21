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

     //Read
    public async Task<UserDto> GetUserAsync(string username)
    {
        return await _userManager.Users
                                 .Where(x => x.UserName == username)
                                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync();
    }

    public async Task<UserDto> GetByIdUserAsync(string id)
    {
        return await _userManager.Users
                                 .Where(x => x.Id == int.Parse(id))
                                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync();
    }

    public async Task<PagedList<UserDto>> GetUsersAsync(AppUserParams userParams)
    {
        var query = _userManager.Users
                        .OrderBy(u => u.Id)
                        .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                        .AsNoTracking();

        return await PagedList<UserDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize, userParams.FilterOptions2);
    }

    public async Task<List<MemberSimpleDto>> GetListUsersAsync()
    {
        return await _context.DB_AppUser
                             .ProjectTo<MemberSimpleDto>(_mapper.ConfigurationProvider)
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

    public async Task<AppUser> GetUserByUsernameAsync(string UserName)
    {
        return await _context.DB_AppUser.SingleOrDefaultAsync(x => x.UserName == UserName);
    }

    //Update
    public void UpdateUser(AppUser appUser)
    {
        _context.Entry(appUser).State = EntityState.Modified;
    }


    //Functions
    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }



}