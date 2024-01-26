using AutoMapper;
using EmployeeCrud.Models.Models;
using EmployeeCrud.Services.DTO;

namespace EmployeeCrud.Services.AutoMapperProfile
{
    public class MapperProfile:Profile
    {
       public MapperProfile() 
        {
            CreateMap<Employee, Employee>();
            CreateMap<AddEmployeeDTO,Employee>();
            CreateMap<AddEmployeeDTO,EmployeeHobby>();
            CreateMap<EmployeeHobby,EmployeeHobby>();
            //CreateMap<UpdateEmployeeDTO, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDTO, Employee>()
               .ForMember(dest => dest.EmployeeHobbies, opt => opt.MapFrom(src => src.EmployeeHobbies.Select(id => new EmployeeHobby { Id = id })));


            CreateMap<Employee,EmployeeHobby>();
            CreateMap<UpdateEmployeeDTO,EmployeeHobby>();
        }
    }

    }

