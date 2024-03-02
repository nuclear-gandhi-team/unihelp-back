using UniHelp.Domain.Entities;

namespace UniHelp.Services.Interfaces.Repositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<List<Comment>> GetCommentsByParentCommentId(int parentCommentId);
}