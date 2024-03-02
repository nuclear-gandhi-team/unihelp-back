using System.Globalization;
using AutoMapper;
using UniHelp.Domain.Entities;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Features.UserFeatures.Dtos;

namespace UniHelp.Features.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.UserName,
                opt =>opt.MapFrom(src => src.FirstName + src.LastName))
            .ReverseMap();
        
        CreateMap<RegisterStudentDto, User>()
            .ForMember(dest => dest.UserName,
                opt =>opt.MapFrom(src => src.FirstName + src.LastName))
            .ReverseMap();
        
        CreateMap<RegisterStudentDto, Student>().ReverseMap();
        CreateMap<Class, GetClassDto>()
            .ForMember(
                dest => dest.ClassName,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.ClassDescription,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(
                dest => dest.ClassesNumber,
                opt => opt.MapFrom(src => src.ClassesNumber))
            .ForMember(
                dest => dest.TeacherId,
                opt => opt.MapFrom(src => src.TeacherId));

        CreateMap<AddClassDto, Class>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.ClassName))
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.ClassDescription))
            .ForMember(
                dest => dest.ClassesNumber,
                opt => opt.MapFrom(src => src.ClassesNumber))
            .ReverseMap();
    }
    
    private static DateTime? ParseDateTime(string dateString)
    {
        return DateTime.TryParseExact(
            dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)
            ? result
            : null;
    }
}