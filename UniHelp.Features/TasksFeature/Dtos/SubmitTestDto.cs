using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.TasksFeature.Dtos;

public record SubmitTestDto
{
    [Required]
    public int TaskId { get; set; }
    
    [Required]
    public List<TestAnswersDto> Answers { get; set; } = default!;
}