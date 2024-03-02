using Microsoft.AspNetCore.Mvc;
using UniHelp.Features.CommentFeatures;
using UniHelp.Services.Interfaces;
using UniHelp.WebAPI.Extensions;

namespace UniHelp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpGet("get-comments/{taskId}")]
    public async Task<IActionResult> GetAllTaskComments(int taskId)
    {
        var comments = await _commentService.GetAllTaskCommentsAsync(taskId);
        return Ok(comments);
    }
    
    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment([FromBody] CreateCommentDto createCommentDto)
    {
        await _commentService.AddCommentAsync(createCommentDto, (await this.GetUserIdFromJwtAsync())!);
        return Ok();
    }
    
    [HttpDelete("remove-comment/{commentId}")]
    public async Task<IActionResult> RemoveComment(int commentId)
    {
        await _commentService.RemoveCommentAsync(commentId, (await this.GetUserIdFromJwtAsync())!);
        return Ok();
    }
}