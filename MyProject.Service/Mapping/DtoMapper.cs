using AutoMapper;
using MyProject.Core.DTOs.AuthDtos;
using MyProject.Core.DTOs.CityDtos;
using MyProject.Core.DTOs.UserDtos;
using MyProject.Core.Entities;

namespace MyProject.Service.Mapping
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<CityUpdateDto, City>().ReverseMap();
            CreateMap<AuthLoginDto, User>().ReverseMap();
            CreateMap<UserUpdateDto, User>().ReverseMap();
            CreateMap<UserRegisterDto, User>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
