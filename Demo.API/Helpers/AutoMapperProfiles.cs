using AutoMapper;
using Demo.API.Dtos;
using Demo.API.Models;

namespace Demo.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserToReturnDto>().ReverseMap();
            CreateMap<UserForLoginDto, User>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<ValueForPostDto, Value>();
            CreateMap<PermForPostDto, Perm>();


        }
    }
}