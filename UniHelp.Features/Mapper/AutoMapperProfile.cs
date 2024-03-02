using System.Globalization;
using AutoMapper;
using UniHelp.Domain.Entities;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Features.StudentFeatures.Dtos;
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
        
        CreateMap<AddStudentToClassDto, StudentClass>()
            .ForMember(
                dest => dest.StudentId,
                opt => opt.MapFrom(src => src.StudentId))
            .ForMember(
                dest => dest.ClassId,
                opt => opt.MapFrom(src => src.ClassId))
            .ReverseMap();
        
        CreateMap<GetAllDto, Student>()
            .ForPath(
                dest => dest.User.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForPath(
                dest => dest.User.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(
                dest => dest.Group,
                opt => opt.MapFrom(src => src.Group))
            .ReverseMap();

        CreateMap<GetOneDto, Student>()
            .ForPath(
                dest => dest.User.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForPath(
                dest => dest.User.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(
                dest => dest.Group,
                opt => opt.MapFrom(src => src.Group))
            .ForPath(
                dest => dest.User.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(
                dest => dest.Course,
                opt => opt.MapFrom(src => src.Course))
            .ForMember(
                dest => dest.Faculty,
                opt => opt.MapFrom(src => src.Faculty))
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