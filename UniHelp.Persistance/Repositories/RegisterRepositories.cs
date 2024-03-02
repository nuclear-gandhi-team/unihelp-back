using Microsoft.Extensions.DependencyInjection;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public static class RegisterRepositories
{
    public static void AddScopedRepositories(this IServiceCollection builder)
    {
        builder.AddScoped<IStudentRepository, StudentRepository>();
        builder.AddScoped<IStudentClassRepository, StudentClassRepository>();
        builder.AddScoped<IAnswerVariantRepository, AnswerVariantRepository>();
        builder.AddScoped<ITaskRepository, TaskRepository>();
        builder.AddScoped<IStudentTaskRepository, StudentTaskRepository>();
        builder.AddScoped<ITeacherRepository, TeacherRepository>();
        builder.AddScoped<IClassRepository, ClassRepository>();
        builder.AddScoped<IUserRepository, UserRepository>();
        builder.AddScoped<ITestQuestionRepository, TestQuestionRepository>();
        builder.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}