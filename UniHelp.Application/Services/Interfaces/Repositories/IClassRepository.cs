using UniHelp.Domain.Entities;
using Task = UniHelp.Domain.Entities.Task;

namespace UniHelp.Services.Interfaces.Repositories;

public interface IClassRepository : IRepository<Class>
{
    Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(int teacherId);
    
    Task<Class> GetClassWithStudentsAsync(int id);
    
    Task<int> GetStudentsOnClassCountAsync(int id);

    Task<string> GetFullTeacherNameByClassAsync(int classId);
}