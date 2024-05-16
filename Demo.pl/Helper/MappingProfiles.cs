using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
            CreateMap<Department,DepartmentViewModel>().ReverseMap();
            CreateMap<ApplicationUser,UserViewModel>().ReverseMap();
            CreateMap<IdentityRole,RoleViewModel>().ReverseMap();
            
            
        }
    }
}
