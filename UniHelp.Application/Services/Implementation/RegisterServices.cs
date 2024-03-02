using Microsoft.Extensions.DependencyInjection;
using UniHelp.Services.Interfaces;

namespace UniHelp.Services.Implementation;

public static class RegisterServices
{
    public static void AddScopedServices(this IServiceCollection builder)
    {
        builder.AddScoped<IUserService, UserService>();
        builder.AddScoped<ITaskService, TaskService>();
        builder.AddScoped<IClassService, ClassService>();
        builder.AddScoped<IStudentService, StudentService>();
        builder.AddScoped<ICommentService, CommentService>();
    }
}