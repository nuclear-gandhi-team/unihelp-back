using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class ClassRepository : Repository<Class>, IClassRepository
{
    public ClassRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
    }
}