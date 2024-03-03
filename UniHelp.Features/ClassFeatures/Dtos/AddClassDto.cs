using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.ClassFeatures.Dtos;

public class AddClassDto
{
    [Required]
    public string ClassName { get; set; } = default!;

    [Required]
    public string ClassDescription { get; set; } = default!;
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
    public int ClassesNumber { get; set; }
}