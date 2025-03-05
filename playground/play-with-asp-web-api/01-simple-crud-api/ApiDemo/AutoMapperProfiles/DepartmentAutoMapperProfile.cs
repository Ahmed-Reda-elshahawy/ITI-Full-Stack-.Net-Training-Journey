using ApiDemo.Dtos.Departments;
using ApiDemo.Models;
using AutoMapper;

namespace ApiDemo.AutoMapperProfiles;

public class DepartmentAutoMapperProfile:Profile
{
    public DepartmentAutoMapperProfile()
    {
        CreateMap<Department, GetDepartmentResponseDto>()
            .ForMember(dest => dest.StudnetCount, opt => opt.MapFrom(src => src.Students.Count()));
        CreateMap<CreateDepartmentRequestDto, Department>();
        CreateMap<UpdateDepartmentRequestDto, Department>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
            .ForMember(dest => dest.Capacity, opt => opt.Condition(src => src.Capacity.HasValue));
    }
}
