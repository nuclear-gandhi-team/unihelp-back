using UniHelp.Features.TasksFeature.Dtos;
using Microsoft.AspNetCore.Http;

namespace UniHelp.Services.Interfaces;

public interface ITaskService
{
    Task AddTaskAsync(AddTaskDto addTaskDto);

    Task SubmitTaskAsync(SubmitTaskDto submitTaskDto, string studentId);

    Task SubmitTestAsync(SubmitTestDto submitTaskDto, string studentId);

    Task SetGradeAsync(SetGradeDto setGradeDto);

    Task<GetTaskDto> GetClosestTaskAsync(int classId);

    Task<IEnumerable<GetTableTaskDto>> GetTasksByClassAndUserAsync(string userId);
    
    Task<IEnumerable<GetTableTaskDto>> GetTasksByClassAsync(int classId);
}