using AutoMapper;
using UserPortal.Application.DTOs.Request;
using UserPortal.Application.DTOs.Response;
using UserPortal.Domain.Entities;

namespace UserPortal.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequestDto, UserEntity>();
            CreateMap<UpdateUserRequestDto, UserEntity>();
            CreateMap<UserEntity, UserResponseDto>();
        }
    }
}
