using AutoMapper;
using Business.Models;
using Data.Domain.Entities;

namespace Business_Logic_Layer.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // auth
            CreateMap<RegistrationModel, User>()
               .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
               .ForMember(u => u.PasswordHash, opt => opt.MapFrom(x => x.Password));
            CreateMap<LoginModel, User>()
                .ForMember(u => u.PasswordHash, opt => opt.MapFrom(x => x.Password));

            // employee
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<Department, DepartmentModel>();
        }
    }
}
