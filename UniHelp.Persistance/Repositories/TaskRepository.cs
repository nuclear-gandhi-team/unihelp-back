using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;
using TaskEntity = UniHelp.Domain.Entities.Task;

namespace UniHelp.Persistance.Repositories;

public class TaskRepository : Repository<TaskEntity>, ITaskRepository
{
    public TaskRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
    }
}