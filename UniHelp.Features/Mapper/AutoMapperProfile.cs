using System.Globalization;
using AutoMapper;
using UniHelp.Domain.Entities;
using UniHelp.Features.TasksFeature.Dtos;
using UniHelp.Features.UserFeatures.Dtos;
using Task = UniHelp.Domain.Entities.Task;

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

        CreateMap<AddTaskDto, Task>()
            .ForMember(dest => dest.TestQuestions,
            opt =>opt.MapFrom(_ =>new List<TestQuestion>()))
            .ReverseMap();
        
        CreateMap<AddTestQuestionDto, TestQuestion>()
            .ForMember(dest => dest.Question,
                opt =>opt.MapFrom(src => src.Question))
            .ReverseMap();

        CreateMap<GetTaskDto, Task>().ReverseMap();
    }
    
    private static DateTime? ParseDateTime(string dateString)
    {
        return DateTime.TryParseExact(
            dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)
            ? result
            : null;
    }
}