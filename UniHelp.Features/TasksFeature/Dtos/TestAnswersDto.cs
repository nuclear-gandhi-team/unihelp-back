using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.TasksFeature.Dtos;

public record TestAnswersDto
{
    [Required]
    public int QuestionId { get; set; }
    
    [Required]
    public int VariantId { get; set; }
}