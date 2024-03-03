using Microsoft.EntityFrameworkCore;
using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class StudentClassRepository : Repository<StudentClass>, IStudentClassRepository
{
    private readonly UniDataContext _dbContext;
    public StudentClassRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<StudentClass?> GetStudentClassAsync(int studentId, int classId)
    {
        return _dbContext.StudentClasses.Include(sc => sc.Student).Include(sc => sc.Class) .ThenInclude(c => c.Tasks)
            .ThenInclude(t => t.StudentTasks)
            .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.ClassId == classId);
    }
}