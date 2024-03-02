using UniHelp.Domain.Entities;

namespace UniHelp.Services.Interfaces.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    Task<IEnumerable<Student>> GetStudentsByClassIdAsync(int classId);
    
    Task<Student> GetStudentByIdWithUserAsync(int id);
    
    Task<IEnumerable<Student>> GetAllStudentsWithUserAsync();
    
    Task<Student> GetStudentWithClassesAsync(int id);
}