using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Model;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            // <From, To>
            #region AppUser
            CreateMap<UserDto, AppUser>();
            CreateMap<AppUser, UserDto>();

            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, RegisterDto>();

            CreateMap<MemberSimpleDto, AppUser>();
            CreateMap<AppUser, MemberSimpleDto>();
            
            CreateMap<UserUpdateDto, AppUser>();
            CreateMap<AppUser, UserUpdateDto>();
            
            CreateMap<LoginDto, AppUser>();

            CreateMap<AppUser, MemberDto>();
            CreateMap<MemberDto, AppUser>();
            #endregion

            #region Warning Level
            #endregion
            
        }
    }
}