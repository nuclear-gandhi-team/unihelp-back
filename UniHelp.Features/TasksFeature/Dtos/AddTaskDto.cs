using System.ComponentModel.DataAnnotations;
using UniHelp.Domain.Entities;

namespace UniHelp.Features.TasksFeature.Dtos;

public record AddTaskDto
{
    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;
    
    [Required]
    public int MaxPoints { get; set; }
    
    [Required]
    public DateTime DateStart { get; set; }
    
    [Required]
    public DateTime DateEnd { get; set; }
    
    [Required]
    public int ClassId { get; set; }

    [Required]
    public string Type { get; set; } = default!;

    public AddTestQuestionDto[]? TestQuestions { get; set; }
}