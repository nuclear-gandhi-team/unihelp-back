using Microsoft.EntityFrameworkCore;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;
using TaskEntity = UniHelp.Domain.Entities.Task;

namespace UniHelp.Persistance.Repositories;

public class TaskRepository : Repository<TaskEntity>, ITaskRepository
{
    private readonly UniDataContext _dbContext;
    public TaskRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskEntity> GetByIdWithCommentsAsync(int id)
    {
        var task = await _dbContext.Tasks
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == id);
        return task;
    }
}