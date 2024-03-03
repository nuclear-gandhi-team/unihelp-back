using UniHelp.Domain.Entities;

namespace UniHelp.Services.Interfaces.Repositories;

public interface IStudentTaskRepository : IRepository<StudentTask>
{
    Task<StudentTask> GetStudentTaskByIdAsync(int studentId, int taskId);
}