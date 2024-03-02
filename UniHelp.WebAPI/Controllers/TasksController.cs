using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHelp.Features.Constants;
using UniHelp.Features.TasksFeature.Dtos;
using UniHelp.Services.Interfaces;
using UniHelp.WebAPI.Extensions;

namespace UniHelp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    [Route("add-task")]
    [Authorize(Roles = UserRoleNames.Teacher)]
    public async Task<IActionResult> AddTaskAsync([FromBody] AddTaskDto addTaskDto)
    {
        await _taskService.AddTaskAsync(addTaskDto);

        return Ok();
    }
    
    [HttpPost]
    [Route("submit-task")]
    // [Authorize(Roles = UserRoleNames.Student)]
    public async Task<IActionResult> SubmitTaskAsync([FromForm] SubmitTaskDto submitTaskDto, [FromForm] IFormFile file)
    {
        var userId = await this.GetUserIdFromJwtAsync();
        if (userId is null)
        {
            return Unauthorized();
        }
        
        await _taskService.SubmitTaskAsync(submitTaskDto, file, "");

        return Ok();
    }
    
    [HttpPost]
    [Route("submit-test")]
    [Authorize(Roles = UserRoleNames.Student)]
    public async Task<IActionResult> SubmitTestAsync([FromForm] SubmitTestDto submitTaskDto)
    {
        var userId = await this.GetUserIdFromJwtAsync();
        if (userId is null)
        {
            return Unauthorized();
        }
        
        await _taskService.SubmitTestAsync(submitTaskDto, userId);

        return Ok();
    }
}