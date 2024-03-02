using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.TasksFeature.Dtos;

public record SubmitTestDto
{
    [Required]
    public int TaskId { get; set; }
    
    [Required]
    public List<SubmitTestDto> Answers { get; set; } = default!;
}