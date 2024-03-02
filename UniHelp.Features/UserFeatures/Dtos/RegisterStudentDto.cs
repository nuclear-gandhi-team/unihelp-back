using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.UserFeatures.Dtos;

public record RegisterStudentDto : RegisterUserDto
{
    [Required]
    public string Faculty { get; set; }
    
    [Required]
    public string Group { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
    public int Course { get; set; }
}