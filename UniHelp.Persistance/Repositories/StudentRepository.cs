using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
    }
}