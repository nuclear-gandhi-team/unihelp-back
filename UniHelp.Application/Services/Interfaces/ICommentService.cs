using UniHelp.Features.CommentFeatures;

namespace UniHelp.Services.Interfaces;

public interface ICommentService
{
    Task AddCommentAsync(CreateCommentDto createCommentDto, string userId);

    Task<IList<GetCommentDto>> GetAllTaskCommentsAsync(int taskId);
    
    Task RemoveCommentAsync(int commentId, string userId);
}