using Microsoft.EntityFrameworkCore;
using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    private readonly UniDataContext _dbContext;
    public CommentRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public new async Task<IEnumerable<Comment>> GetAllAsync()
    {
        return await _dbContext.Comments
            .Include(c => c.ParentComment)
            .Include(c => c.User)
            .ToListAsync();
    }
    
    public async Task<List<Comment>> GetCommentsByParentCommentId(int parentCommentId)
    {
        return await _dbContext.Comments
            .Where(c => c.ParentCommentId == parentCommentId)
            .ToListAsync();
    }
}