using System.Globalization;
using AutoMapper;
using UniHelp.Domain.Entities;
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
    }
    
    private static DateTime? ParseDateTime(string dateString)
    {
        return DateTime.TryParseExact(
            dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)
            ? result
            : null;
    }
}