using Microsoft.EntityFrameworkCore;
using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly UniDataContext _dbContext;
    public StudentRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Student>> GetStudentsByClassIdAsync(int classId)
    {
        var students = await _dbContext.Students.Include(s => s.User)
            .Where(s => s.StudentClasses.Any(sc => sc.ClassId == classId))
            .ToListAsync();
        return students;
    }

    public async Task<Student> GetStudentByIdWithUserAsync(int id)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);
        return student;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsWithUserAsync()
    {
        var students = await _dbContext.Students
            .Include(s => s.User)
            .ToListAsync();
        return students;
    }

    public async Task<Student> GetStudentWithClassesAsync(int id)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            .Include(s => s.StudentClasses)
            .ThenInclude(sc => sc.Class)
            .FirstOrDefaultAsync(s => s.Id == id);
        return student;
    }
}