using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class StudentClassRepository : Repository<StudentClass>, IStudentClassRepository
{
    public StudentClassRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
    }
}