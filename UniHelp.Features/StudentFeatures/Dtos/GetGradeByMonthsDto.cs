using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.StudentFeatures.Dtos;

public record GetGradeByMonthsDto
{
    [Required]
    public string Month { get; set; } = default!;
    
    [Required]
    public double Total { get; set; }
}