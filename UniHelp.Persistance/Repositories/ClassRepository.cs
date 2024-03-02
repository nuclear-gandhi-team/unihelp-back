using Microsoft.EntityFrameworkCore;
using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class ClassRepository : Repository<Class>, IClassRepository
{
    private readonly UniDataContext _dbContext;
    public ClassRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(int teacherId)
    {
        var classes = await _dbContext.Classes.Where(c => c.TeacherId == teacherId).ToListAsync();
        return classes;
    }

    public async Task<Class> GetClassWithStudentsAsync(int id)
    {
        var classEntity = await _dbContext.Classes
            .Include(c => c.StudentClasses)
            .ThenInclude(sc => sc.Student)
            .FirstOrDefaultAsync(c => c.Id == id);
        return classEntity;
    }
}