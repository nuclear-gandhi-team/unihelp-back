using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class StudentTaskRepository : Repository<StudentTask>, IStudentTaskRepository
{
    public StudentTaskRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
    }
}