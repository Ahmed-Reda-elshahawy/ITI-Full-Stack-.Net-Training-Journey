using ApiDemo.Dtos.Students;
using ApiDemo.Models;
using AutoMapper;

namespace ApiDemo.AutoMapperProfiles;

public class StudentsAutoMapperProfile:Profile
{
    public StudentsAutoMapperProfile()
    {
        CreateMap<Student, GetStudentResponseDto>()
            .ForMember(
                dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department.Name)
                );

        CreateMap<CreateStudentRequestDto, Student>();

        CreateMap<UpdateStudentRequestDto, Student>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id to prevent overwriting
                    .ForMember(dest => dest.Department, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
                    .ForMember(dest => dest.Age, opt => opt.Condition(src => src.Age.HasValue))
                    .ForMember(dest => dest.DeptId, opt => opt.Condition(src => src.DeptId.HasValue));
    }
}
