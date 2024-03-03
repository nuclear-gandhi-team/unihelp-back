using UniHelp.Domain.Entities;

namespace UniHelp.Services.Interfaces.Repositories;

public interface IStudentClassRepository : IRepository<StudentClass>
{
    Task<StudentClass?> GetStudentClassAsync(int studentId, int classId);
}