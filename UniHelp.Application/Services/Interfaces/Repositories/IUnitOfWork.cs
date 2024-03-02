using Task = UniHelp.Domain.Entities.Task;

namespace UniHelp.Services.Interfaces.Repositories;

public interface IUnitOfWork
{
    ITeacherRepository Teachers { get; }
    
    IStudentClassRepository StudentClasses { get; }
    
    ITestQuestionRepository TestQuestions { get; }
    
    IStudentRepository Students { get; }
    
    IAnswerVariantRepository AnswerVariants { get; }
    
    ITaskRepository Tasks { get; }
    
    IStudentTaskRepository StudentTasks { get; }
    
    IClassRepository Classes { get; }
    
    IUserRepository Users { get; }
    
    ICommentRepository Comments { get; }
    
    System.Threading.Tasks.Task CommitAsync();
}