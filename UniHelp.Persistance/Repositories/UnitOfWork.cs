using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly UniDataContext _context;
    private bool _disposed;

    public UnitOfWork(UniDataContext context, ITeacherRepository teachers, IStudentClassRepository studentClasses, ITestQuestionRepository testQuestions, IStudentRepository students, IAnswerVariantRepository answerVariants, ITaskRepository tasks, IStudentTaskRepository studentTasks, IClassRepository classes, IUserRepository users, ICommentRepository comments)
    {
        _context = context;
        Teachers = teachers;
        StudentClasses = studentClasses;
        TestQuestions = testQuestions;
        Students = students;
        AnswerVariants = answerVariants;
        Tasks = tasks;
        StudentTasks = studentTasks;
        Classes = classes;
        Users = users;
        Comments = comments;
    }

    public ITeacherRepository Teachers { get; }
    public IStudentClassRepository StudentClasses { get; }
    public ITestQuestionRepository TestQuestions { get; }
    public IStudentRepository Students { get; }
    public IAnswerVariantRepository AnswerVariants { get; }
    public ITaskRepository Tasks { get; }
    public IStudentTaskRepository StudentTasks { get; }
    public IClassRepository Classes { get; }
    public IUserRepository Users { get; }
    public ICommentRepository Comments { get; }
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
}