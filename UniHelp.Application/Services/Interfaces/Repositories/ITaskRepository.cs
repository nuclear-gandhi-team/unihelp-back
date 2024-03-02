namespace UniHelp.Services.Interfaces.Repositories;
using TaskEntity = UniHelp.Domain.Entities.Task;

public interface ITaskRepository : IRepository<TaskEntity>
{
    Task<TaskEntity> GetByIdWithCommentsAsync(int id);
}