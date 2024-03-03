using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.UserFeatures.Dtos;

public record UpdateUserPasswordDto
{
    [Required]
    public string OldPassword { get; set; } = default!;
    
    [Required]
    public string NewPassword { get; set; } = default!;
    
    [Required]
    public string ConfirmNewPassword { get; set; } = default!;
}