using AutoMapper;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;

namespace ISD_Project.Server.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Source, Destination>();
            //Model -> DTO
            CreateMap<User, UserDto>();
            CreateMap<Customer, UserDto>();
            CreateMap<CustomerCareDepartment, UserDto>();
            //DTO -> Model
            CreateMap<UserDto, User>();
            CreateMap<UserDto, Customer>();
            CreateMap<UserDto, CustomerCareDepartment>();
            
        }
    }
}
