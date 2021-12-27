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

            #region Event
            CreateMap<EventDB, EventSimpleDto>();
            CreateMap<EventState, EventStateEmptyDto>();
            CreateMap<EventType, EventTypeEmptyDto>();
            CreateMap<EventDB, EventDisplayDto>()
                .ForMember(e => e.sport, ex => ex.MapFrom(e => e.sport.Description))
                .ForMember(eD => eD.state, ex => ex.MapFrom(e => e.eventState.Description))
                .ForMember(eD => eD.type, ex => ex.MapFrom(e => e.eventType.Description));
            #endregion

            #region Bet
            CreateMap<Bet, BetEmptyDto>();
            CreateMap<CreateBetDTO, Bet>();
            #endregion

            #region Sport
            CreateMap<Sport, SportEmptyDto>();
            #endregion
            
        }
    }
}