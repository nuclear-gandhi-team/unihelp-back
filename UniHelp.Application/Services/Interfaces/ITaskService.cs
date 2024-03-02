using UniHelp.Features.TasksFeature.Dtos;
using Microsoft.AspNetCore.Http;

namespace UniHelp.Services.Interfaces;

public interface ITaskService
{
    Task AddTaskAsync(AddTaskDto addTaskDto);

    Task SubmitTaskAsync(SubmitTaskDto submitTaskDto, IFormFile file, string studentId);

    Task SubmitTestAsync(SubmitTestDto submitTaskDto, string studentId);
}