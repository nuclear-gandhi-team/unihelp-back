using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.TasksFeature.Dtos;

public record SetGradeDto
{
    [Required]
    public int TaskId { get; set; }
    
    [Required]
    public int Grade { get; set; }
    
    [Required]
    public int StudentId { get; set; }
}