using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.ClassFeatures.Dtos;

public class AddClassDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
    public string ClassName { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
    public string ClassDescription { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
    public int ClassesNumber { get; set; }
}