using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.UserFeatures.Dtos;

public record RegisterUserDto
{
    [Required]
    public string FirstName { get; set; } = default!;
    
    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;
    
    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string ConfirmPassword { get; set; } = default!;
}