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
            CreateMap<Customer, CustomerDto>();
            CreateMap<ValidationDepartment, UserDto>();
            CreateMap<CustomerCareDepartment, UserDto>();
            CreateMap<FinancialDepartment, UserDto>();
            CreateMap<Admin, UserDto>();
            CreateMap<HealthInformation, HealthInformationDto>();
            //DTO -> Model
            CreateMap<UserDto, User>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<UserDto, ValidationDepartment>();
            CreateMap<UserDto, CustomerCareDepartment>();
            CreateMap<UserDto, FinancialDepartment>();
            CreateMap<UserDto, Admin>();
            CreateMap<HealthInformationDto, HealthInformation>();

            
        }
    }
}
