using System.Globalization;
using AutoMapper;
using UniHelp.Domain.Entities;
using UniHelp.Domain.Enums;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Features.CommentFeatures;
using UniHelp.Features.Constants;
using UniHelp.Features.StudentFeatures.Dtos;
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
        
        CreateMap<Comment, GetCommentDto>()
            .ForMember(dest => dest.Body,
                opt => opt.MapFrom(src => ChangeCommentBody(src)))
            .ForMember(
                dest => dest.ChildComments,
                opt => opt.MapFrom(src => src.SubComments))
            .ForPath(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User.UserName))
            .ReverseMap();

        CreateMap<CreateCommentDto, Comment>()
            .ForMember(
                dest => dest.Body,
                opt => opt.MapFrom(src => src.Body))
            .ForMember(
                dest => dest.TaskId,
                opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dest => dest.Action,
                opt => opt.MapFrom(src => src.Action))
            .ReverseMap();

        CreateMap<Task, GetTableTaskDto>()
            .ForMember(
                dest => dest.ClassName,
                opt => opt.MapFrom(src => src.Class.Name))
            .ReverseMap();

    }
    
    private static DateTime? ParseDateTime(string dateString)
    {
        return DateTime.TryParseExact(
            dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)
            ? result
            : null;
    }
    
    private static string ChangeCommentBody(Comment comment)
    {
        return comment.IsRemoved
            ? CommentBody.DeletedCommentBody
            : comment.Action switch
            {
                CommentTypes.Reply => string.Format(CommentBody.ReplyBodyTemplate, comment.Task.Name, comment.Id, comment.User.UserName, comment.Body),
                CommentTypes.Quote => string.Format(CommentBody.QuoteBodyTemplate, comment.Task.Name, comment.Id, comment.User.UserName, comment.Body, comment.ParentComment?.Body),
                CommentTypes.Comment => comment.Body,
                CommentTypes.Nothing => comment.Body,
                _ => throw new ArgumentOutOfRangeException(),
            };
    }
}