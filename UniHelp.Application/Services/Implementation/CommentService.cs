using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniHelp.Domain.Entities;
using UniHelp.Features.CommentFeatures;
using UniHelp.Features.Exceptions;
using UniHelp.Services.Interfaces;
using UniHelp.Services.Interfaces.Repositories;
using Task = System.Threading.Tasks.Task;

namespace UniHelp.Services.Implementation;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task AddCommentAsync(CreateCommentDto createCommentDto, string userId)
    {
        if (await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId) is null)
        {
            throw new EntityNotFoundException($"No user with Id '{userId}'");
        }
        var task = await _unitOfWork.Tasks.GetByIdAsync(createCommentDto.TaskId) 
                   ?? throw new EntityNotFoundException($"No task with Id '{createCommentDto.TaskId}'");
        
        if (task.DateEnd > DateTime.Now)
        {
            throw new TaskIsNotFinishedException("Task is not finished yet, you cannot add comment");
        }
        if (createCommentDto.Body.IsNullOrEmpty())
        {
            throw new EmptyCommentException("Comment body cannot be empty");
        }
        var comment = new Comment
        {
            Body = createCommentDto.Body,
            TaskId = createCommentDto.TaskId,
            UserId = userId
        };
        if (createCommentDto.ParentCommentId.HasValue)
        {
            var parentComment = await _unitOfWork.Comments.GetByIdAsync(createCommentDto.ParentCommentId.Value) 
                                ?? throw new EntityNotFoundException($"No comment with Id '{createCommentDto.ParentCommentId.Value}'");
            if (parentComment.IsRemoved)
            {
                throw new CommentIsRemovedException("You cannot add comment to removed comment");
            }
            comment.ParentCommentId = parentComment.Id;
        }
        
        await _unitOfWork.Comments.AddAsync(comment);
        await _unitOfWork.CommitAsync();

    }

    public async Task<IList<GetCommentDto>> GetAllTaskCommentsAsync(int taskId)
    {
        var task = await _unitOfWork.Tasks.GetByIdWithCommentsAsync(taskId) 
                   ?? throw new EntityNotFoundException($"No task with Id '{taskId}'");
        var comments = (await _unitOfWork.Comments.GetAllAsync())
            .Where(c => c.TaskId == task.Id && c.ParentCommentId is null)
            .ToList();
        var commentsDto = _mapper.Map<IList<GetCommentDto>>(comments);
        
        return commentsDto;
    }

    public async Task RemoveCommentAsync(int commentId, string userId)
    {
        if (await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId) is null)
        {
            throw new EntityNotFoundException($"No user with Id '{userId}'");
        }
        
        if (commentId <= 0)
        {
            throw new EntityNotFoundException($"No comment with Id '{commentId}'");
        }
        
        var comment = await _unitOfWork.Comments.GetByIdAsync(commentId) 
                     ?? throw new EntityNotFoundException($"No comment with Id '{commentId}'");
        
        if (comment.UserId != userId &&
            !await _userManager.IsInRoleAsync((await _userManager.FindByIdAsync(userId))!, "Teacher"))
        {
            throw new AccessDeniedException("You cannot remove comment that is not yours");
        }

        if (comment.IsRemoved)
        {
            throw new CommentIsRemovedException("Comment is already removed");
        }

        comment.IsRemoved = true;
        await _unitOfWork.Comments.UpdateAsync(comment);
        await _unitOfWork.CommitAsync();
    }
}