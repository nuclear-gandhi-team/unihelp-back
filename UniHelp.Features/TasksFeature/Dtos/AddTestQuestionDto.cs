using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.TasksFeature.Dtos;

public record AddTestQuestionDto
{
    [Required]
    public string Question { get; set; } = default!;
    
    [Required]
    public virtual IList<string> AnswerVariants { get; set; } = default!;
    
    [Required]
    public int CorrectAnswer { get; set; } = default!;
}