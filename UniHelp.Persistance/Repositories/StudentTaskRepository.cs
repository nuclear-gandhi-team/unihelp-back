using Microsoft.EntityFrameworkCore;
using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class StudentTaskRepository : Repository<StudentTask>, IStudentTaskRepository
{
    private readonly UniDataContext _dbContext;
    public StudentTaskRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<StudentTask> GetStudentTaskByIdAsync(int studentId, int taskId)
    {
        var studentTask = _dbContext.StudentTasks
            .Include(st => st.Student)
            .Include(st => st.Task)
            .FirstOrDefaultAsync(st => st.StudentId == studentId && st.TaskId == taskId);
        return studentTask;
    }
}