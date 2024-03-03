using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UniHelp.Features.TasksFeature.Dtos;

public record SubmitTaskDto
{
    [Required]
    public int TaskId { get; set; }

    [Required]
    public IFormFile File { get; set; } = default!;
}