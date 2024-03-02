using UniHelp.Domain.Entities;

namespace UniHelp.Services.Interfaces.Repositories;

public interface IClassRepository : IRepository<Class>
{
    Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(int teacherId);
    
    Task<Class> GetClassWithStudentsAsync(int id);
}